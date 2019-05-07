using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using NodeModel;

using Windows.Storage;
using Windows.Storage.Streams;

namespace NodeRepository
{
    public partial class Repository
    {
        #region Read  =========================================================
        public async void Read(Chef chef)
        {
            try
            {
                using (var stream = await _storageFile.OpenAsync(FileAccessMode.Read))
                {
                    using (DataReader r = new DataReader(stream))
                    {
                        r.ByteOrder = ByteOrder.LittleEndian;
                        UInt64 size = stream.Size;
                        if (size < UInt32.MaxValue)
                        {
                            var byteCount = await r.LoadAsync((UInt32)size);
                            Read(chef, r);
                        }
                    }
                }
                chef.AfterReadValidation();
            }
            catch (Exception ex)
            {
                chef.AddRepositorReadError(ex.Message);
            }
        }
        #endregion

        #region Read  =========================================================
        private void Read(Chef chef, DataReader r)
        {
            // determine the data file format
            var spacer = r.ReadInt32();
            var fileFormat = r.ReadGuid();

            Item[] items = null;
            Action<Chef, DataReader, Item[]>[] vector = null;

            if (spacer == 0)
            {
                if (fileFormat == _fileFormat_1)
                {
                    var (_, itemIndex) = chef.GetItemIndex();
                    items = ReadHeader_1(r, chef, itemIndex );
                    vector = new Action<Chef, DataReader, Item[]>[]
                    {
                        null,                // 0
                        null,                // 1
                        null,                // 2
                        ReadTableX_1,        // 3 TableX
                        null,                // 4 
                        null,                // 5 
                        null,                // 6 
                        ReadColumnX_1,       // 7 ColumnX
                        null,                // 8 
                        null,                // 9 
                        ReadRelationX_1,     // 10 RelationX
                        null,                // 11 
                        ReadRelationLink_1,  // 12 RelationLink
                    };
                }
            }

            if (vector == null) throw new Exception($"Unkown File Format Id {fileFormat}");

            for (; ; )
            {
                var mark = (Mark)r.ReadByte();
                var vect = (int)mark;
                if (mark == Mark.StorageFileEnding)
                {
                    var format = r.ReadGuid();
                    if (format != fileFormat) throw new Exception($"Ending Format Id Does Not Match {format}");
                    return; // appearently there were no errors!
                }
                else if (vect > 0 && vect < vector.Length)
                {
                    vector[vect](chef, r, items);
                }
                else
                {
                    throw new Exception($"Invalid Marker {mark}");
                }
            }
        }
        #endregion

        #region ReadHeader_1  =================================================
        private Item[] ReadHeader_1(DataReader r, Chef chef, Dictionary<Item, int> itemIndex)
        {
            var count = r.ReadInt32();

            var items = new Item[count];

            // populate with known items
            //===========================
            foreach (var e in itemIndex)
            {
                var index = e.Value;
                items[index] = e.Key;
            }

            return items;
        }
        #endregion


        #region ReadTableX_1  =================================================
        private void ReadTableX_1(Chef chef, DataReader r, Item[] items)
        {
            var store = chef.TableXStore;
            var count = r.ReadInt32();
            if (count < 0) throw new Exception($"Invalid count {count}");

            for (int i = 0; i < count; i++)
            {
                var index = r.ReadInt32();
                if (index < 0 || index >= items.Length) throw new Exception($"Invalid table index {index}");

                var tx = new TableX(store);
                items[index] = tx;

                var b = r.ReadByte();
                if ((b & B1) != 0) tx.Name = ReadString(r);
                if ((b & B2) != 0) tx.Summary = ReadString(r);
                if ((b & B3) != 0) tx.Description = ReadString(r);

                tx.Center = (r.ReadSingle(), r.ReadSingle());
                tx.Radius = (r.ReadByte(), r.ReadByte());

                var rxCount = r.ReadInt32();
                if (rxCount < 0) throw new Exception($"Invalid row count {count}");
                if (rxCount > 0) tx.SetCapacity(rxCount);

                for (int j = 0; j < rxCount; j++)
                {
                    var index2 = r.ReadInt32();
                    if (index2 < 0 || index2 >= items.Length) throw new Exception($"Invalid row index {index2}");

                    var rx = new RowX(tx);
                    items[index2] = rx;

                    //in this application each row is a graph node
                    rx.Center= (r.ReadSingle(), r.ReadSingle());
                }
            }
            var mark = (Mark)r.ReadByte();
            if (mark != Mark.TableXEnding) throw new Exception($"Expected TableXEnding marker, instead got {mark}");
        }
        #endregion

        #region ReadColumnX_1  ================================================
        private void ReadColumnX_1(Chef chef, DataReader r, Item[] items)
        {
            var store = chef.ColumnXStore;
            var count = r.ReadInt32();
            if (count < 0) throw new Exception($"Invalid count {count}");

            for (int i = 0; i < count; i++)
            {
                var index = r.ReadInt32();
                if (index < 0 || index >= items.Length) throw new Exception($"Invalid column index {index}");

                var cx = new ColumnX(store);
                items[index] = cx;

                var b = r.ReadByte();
                if ((b & B1) != 0) cx.SetState(r.ReadUInt16());
                if ((b & B2) != 0) cx.Name = ReadString(r);
                if ((b & B3) != 0) cx.Summary = ReadString(r);
                if ((b & B5) != 0) cx.Description = ReadString(r);

                var t = (ValType)r.ReadByte();

                ReadValueDictionary(r, t, cx, items);
            }
            var mark = (Mark)r.ReadByte();
            if (mark != Mark.ColumnXEnding) throw new Exception($"Expected ColumnXEnding marker, instead got {mark}");
        }
        #endregion

        #region ReadRelationX_1  ==============================================
        private void ReadRelationX_1(Chef chef, DataReader r, Item[] items)
        {
            var store = chef.RelationXStore;
            var count = r.ReadInt32();
            if (count < 0) throw new Exception($"Invalid count {count}");

            for (int i = 0; i < count; i++)
            {
                var index = r.ReadInt32();
                if (index < 0 || index >= items.Length) throw new Exception($"Invalid index {index}");

                var rx = new RelationX(store);
                items[index] = rx;

                var b = r.ReadByte();
                if ((b & B1) != 0) rx.SetState(r.ReadUInt16());
                if ((b & B2) != 0) rx.Name = ReadString(r);
                if ((b & B3) != 0) rx.Summary = ReadString(r);
                if ((b & B4) != 0) rx.Description = ReadString(r);
                if ((b & B5) != 0) rx.Pairing = (Pairing)r.ReadByte();
                if ((b & B6) != 0) r.ReadInt16();
                if ((b & B6) != 0) r.ReadInt16();
                var keyCount = ((b & B7) != 0) ? r.ReadInt32() : 0;
                var valCount = ((b & B7) != 0) ? r.ReadInt32() : 0;
                rx.Initialize(keyCount, valCount);
            }
            var mark = (Mark)r.ReadByte();
            if (mark != Mark.RelationXEnding) throw new Exception($"Expected RelationXEnding marker, instead got {mark}");
        }
        #endregion


        #region ReadRelationLink_1  ===========================================
        private void ReadRelationLink_1(Chef chef, DataReader r, Item[] items)
        {
            var index = r.ReadInt32();
            var count = r.ReadInt32();

            if (index < 0 || index >= items.Length) throw new Exception($"Invalid relation index {index}");

            var item = items[index];
            if (item == null) throw new Exception($"Relation item is null at index {index}");

            var rel = item as Relation;
            if (rel == null) throw new Exception("Relation item is not a relation");

            for (int i = 0; i < count; i++)
            {
                var index1 = r.ReadInt32();
                var index2 = r.ReadInt32();
                var len = r.ReadUInt16();

                if (index1 < 0 || index1 >= items.Length) throw new Exception($"Invalid index1 {index1}");

                var item1 = items[index1];
                if (item1 == null) throw new Exception($"Related item1 is null at index1 {index1}");

                if (index2 < 0 || index2 >= items.Length) throw new Exception($"Invalid index2 {index2}");

                var item2 = items[index2];
                if (item2 == null) throw new Exception($"Related item2 is null at index2 {index2}");

                 rel.SetLink(item1, item2, len);
            }
            var mark = (Mark)r.ReadByte();
            if (mark != Mark.RelationLinkEnding) throw new Exception($"Expected RelationLinkEnding marker, instead got {mark}");
        }
        #endregion

        #region Read String/Bytes  ============================================
        static string ReadString(DataReader r)
        {
            var len = (UInt32)r.ReadUInt16();
            var str = r.ReadString(len);
            return (str == "^") ? string.Empty : str;
        }
        static byte[] ReadBytes(DataReader r)
        {
            var len = r.ReadInt32();
            var data = new byte[len];
            for (int i = 0; i < len; i++)
            {
                data[i] = r.ReadByte();
            }
            return data;
        }
        #endregion
    }
}
