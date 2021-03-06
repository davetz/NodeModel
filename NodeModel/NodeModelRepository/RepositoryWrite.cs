﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using NodeModel;

using Windows.Storage.Streams;

namespace NodeModelRepository
{
    public partial class Repository
    {
        #region Write  ========================================================
        public async void Write(Chef chef)
        {
            try
            {
                using (var tran = await _storageFile.OpenTransactedWriteAsync())
                {
                    using (var w = new DataWriter(tran.Stream))
                    {
                        w.ByteOrder = ByteOrder.LittleEndian;
                        Write(chef, w);
                        tran.Stream.Size = await w.StoreAsync(); // reset stream size to override the file
                        await tran.CommitAsync();
                    }
                }
            }
            catch (Exception ex)
            {
                chef.AddRepositorWriteError(ex.Message);
            }
        }
        #endregion

        #region Write  ========================================================
        private void Write(Chef chef, DataWriter w)
        {
            var fileFormat = _fileFormat_1;
            var (minIndex, maxIndex, itemIndex) = chef.GetItemIndex();
            var relationList = chef.GetRelationList();

            w.WriteInt32(0);            // file identifier: a zero int spacer
            w.WriteGuid(fileFormat);    // followed by the file format guid

            WriteHeader(chef, w, minIndex, maxIndex);

            if (chef.TableXStore.Count > 0) WriteTableX(chef, w, itemIndex);
            if (chef.ColumnXStore.Count > 0) WriteColumnX(chef, w, itemIndex);
            if (chef.RelationXStore.Count > 0) WriteRelationX(chef, w, itemIndex);

            if (relationList.Count > 0) WriteRelationLink(chef, w, relationList, itemIndex);

            w.WriteByte((byte)Mark.StorageFileEnding);
            w.WriteGuid(fileFormat);
            w.WriteInt32(0);
        }
        #endregion

        #region WriteHeader  ==================================================
        private void WriteHeader(Chef chef, DataWriter w, int minIndex, int maxIndex)
        {
            w.WriteInt32(minIndex); //index of first external item
            w.WriteInt32(maxIndex); //index of last external item
        }
        #endregion


        #region WriteTableX  ==================================================
        private void WriteTableX(Chef chef, DataWriter w, Dictionary<Item, int> itemIndex)
        {
            w.WriteByte((byte)Mark.TableXBegin); // type index
            w.WriteInt32(chef.TableXStore.Count);

            foreach (var tx in chef.TableXStore.Items)
            {
                w.WriteInt32(itemIndex[tx]);

                var b = BZ;
                if (!string.IsNullOrWhiteSpace(tx.Name)) b |= B1;
                if (!string.IsNullOrWhiteSpace(tx.ToolTip)) b |= B2;
                if (!string.IsNullOrWhiteSpace(tx.Description)) b |= B3;

                w.WriteByte(b);
                if ((b & B1) != 0) WriteString(w, tx.Name);
                if ((b & B2) != 0) WriteString(w, tx.ToolTip);
                if ((b & B3) != 0) WriteString(w, tx.Description);

                w.WriteSingle(tx.Center.X);
                w.WriteSingle(tx.Center.Y);
                w.WriteByte(tx.Radius.X);
                w.WriteByte(tx.Radius.Y);

                if (tx.Count > 0)
                {
                    w.WriteInt32(tx.Count);
                    foreach (var rx in tx.Items)
                    {
                        w.WriteInt32(itemIndex[rx]);

                        var d = BZ;

                        if (!string.IsNullOrWhiteSpace(rx.Name)) d |= B1;
                        if (!string.IsNullOrWhiteSpace(rx.ToolTip)) d |= B2;

                        w.WriteByte(d);
                        if ((d & B1) != 0) WriteString(w, rx.Name);
                        if ((d & B2) != 0) WriteString(w, rx.ToolTip);

                        //in this application each row is a graph node
                        w.WriteSingle(rx.Center.X);
                        w.WriteSingle(rx.Center.Y);
                    }
                }
                else
                {
                    w.WriteInt32(0);
                }
            }
            w.WriteByte((byte)Mark.TableXEnding); // itegrity marker
        }
        #endregion

        #region WriteColumnX  =================================================
        private void WriteColumnX(Chef chef, DataWriter w, Dictionary<Item, int> itemIndex)
        {
            w.WriteByte((byte)Mark.ColumnXBegin); // type index
            w.WriteInt32(chef.ColumnXStore.Count);

            foreach (var cx in chef.ColumnXStore.Items)
            {
                w.WriteInt32(itemIndex[cx]);

                var b = BZ;
                if (cx.HasState()) b |= B1;
                if (!string.IsNullOrWhiteSpace(cx.Name)) b |= B2;
                if (!string.IsNullOrWhiteSpace(cx.Summary)) b |= B3;
                if (!string.IsNullOrWhiteSpace(cx.Description)) b |= B4;

                w.WriteByte(b);
                if ((b & B1) != 0) w.WriteUInt16(cx.GetState());
                if ((b & B2) != 0) WriteString(w, cx.Name);
                if ((b & B3) != 0) WriteString(w, cx.Summary);
                if ((b & B5) != 0) WriteString(w, cx.Description);

                w.WriteByte((byte)cx.Value.ValType);

                WriteValueDictionary(w, cx, itemIndex);
            }
            w.WriteByte((byte)Mark.ColumnXEnding); // itegrity marker
        }
        #endregion

        #region WriteRelationX  ===============================================
        private void WriteRelationX(Chef chef, DataWriter w, Dictionary<Item, int> itemIndex)
        {
            w.WriteByte((byte)Mark.RelationXBegin); // type index
            w.WriteInt32(chef.RelationXStore.Count);

            foreach (var rx in chef.RelationXStore.Items)
            {
                w.WriteInt32(itemIndex[rx]);

                var keyCount = rx.KeyCount;
                var valCount = rx.ValueCount;

                var b = SZ;
                if (rx.HasState()) b |= S1;
                if (!string.IsNullOrWhiteSpace(rx.Name)) b |= S2;
                if (!string.IsNullOrWhiteSpace(rx.Summary)) b |= S3;
                if (!string.IsNullOrWhiteSpace(rx.Description)) b |= S4;
                if (rx.Pairing != Pairing.OneToMany) b |= S5;
                if ((keyCount + valCount) > 0) b |= S7;

                var inputPin = rx.InputPin;
                var outputPin = rx.OutputPin;

                if (!string.IsNullOrWhiteSpace(inputPin.Name)) b |= S11;
                if (!string.IsNullOrWhiteSpace(inputPin.Summary)) b |= S12;
                if (!string.IsNullOrWhiteSpace(inputPin.Description)) b |= S13;

                if (!string.IsNullOrWhiteSpace(outputPin.Name)) b |= S14;
                if (!string.IsNullOrWhiteSpace(outputPin.Summary)) b |= S15;
                if (!string.IsNullOrWhiteSpace(outputPin.Description)) b |= S16;

                w.WriteUInt16(b);
                if ((b & S1) != 0) w.WriteUInt16(rx.GetState());
                if ((b & S2) != 0) WriteString(w, rx.Name);
                if ((b & S3) != 0) WriteString(w, rx.Summary);
                if ((b & S4) != 0) WriteString(w, rx.Description);
                if ((b & S5) != 0) w.WriteByte((byte)rx.Pairing);
                if ((b & S7) != 0) w.WriteInt32(keyCount);
                if ((b & S7) != 0) w.WriteInt32(valCount);

                if ((b & S11) != 0) WriteString(w, inputPin.Name);
                if ((b & S12) != 0) WriteString(w, inputPin.Summary);
                if ((b & S13) != 0) WriteString(w, inputPin.Description);

                w.WriteByte(inputPin.Offset.DX);
                w.WriteByte(inputPin.Offset.DY);
                w.WriteByte(inputPin.Size.W);
                w.WriteByte(inputPin.Size.H);

                if ((b & S14) != 0) WriteString(w, outputPin.Name);
                if ((b & S15) != 0) WriteString(w, outputPin.Summary);
                if ((b & S16) != 0) WriteString(w, outputPin.Description);

                w.WriteByte(outputPin.Offset.DX);
                w.WriteByte(outputPin.Offset.DY);
                w.WriteByte(outputPin.Size.W);
                w.WriteByte(outputPin.Size.H);
            }
            w.WriteByte((byte)Mark.RelationXEnding); // itegrity marker
        }
        #endregion


        #region WriteRelationLink  ============================================
        private void WriteRelationLink(Chef chef, DataWriter w, List<Relation> relationList, Dictionary<Item, int> itemIndex)
        {
            foreach (var rx in relationList)
            {
                var count = rx.GetLinksCount();
                if (count == 0) continue;

                ushort len = 0;
                Item itm = chef;
                rx.GetLinks(out List<Item> parents, out List<Item> children);

                int N = count;
                for (int j = 0; j < count; j++)
                {
                    var child = children[j];
                    var parent = parents[j];
                    if (itemIndex.ContainsKey(child) && itemIndex.ContainsKey(parent)) continue;

                    // null out this is link, it should not be serialized
                    children[j] = null;
                    parents[j] = null;
                    N -= 1;
                }
                if (N == 0) continue;

                w.WriteByte((byte)Mark.RelationLinkBegin); // type index
                w.WriteInt32(itemIndex[rx]);
                w.WriteInt32(N);

                for (int j = 0; j < count; j++)
                {
                    var child = children[j];
                    var parent = parents[j];
                    if (child == null || parent == null) continue;

                    if (itm != parent)
                    {
                        len = 1;
                        itm = parent;
                        for (int k = j + 1; k < count; k++)
                        {
                            if (parents[k] != itm) break;
                            if (len < ushort.MaxValue) len += 1;
                        }
                    }

                    w.WriteInt32(itemIndex[parent]);
                    w.WriteInt32(itemIndex[child]);
                    w.WriteUInt16(len);
                    len = 0;
                }
                w.WriteByte((byte)Mark.RelationLinkEnding); // type index
            }
        }
        #endregion

        #region Write String/Bytes  ===========================================
        static void WriteString(DataWriter w, string str)
        {
            var txt = str ?? string.Empty;
            if (txt.Length == 0) txt = "^";

            var len = w.MeasureString(txt);
            if (len > UInt16.MaxValue)
            {
                var r = (double)len / (double)UInt16.MaxValue;
                var n = (UInt16)((txt.Length / r) - 2);
                var trucated = txt.Substring(0, n);
                w.WriteUInt16((UInt16)w.MeasureString(trucated));
                w.WriteString(trucated);
            }
            else
            {
                w.WriteUInt16((UInt16)len);
                w.WriteString(txt);
            }
        }
        static void WriteBytes(DataWriter w, byte[] data)
        {
            var len = data.Length;
            w.WriteInt32(len);
            foreach (var b in data)
            {
                w.WriteByte(b);
            }
        }
        #endregion
    }
}
