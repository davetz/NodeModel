using System;
using System.Threading.Tasks;

using NodeModel;

using Windows.Storage;

namespace NodeRepository
{
    public partial class Repository : IRepository
    {
        StorageFile _storageFile;

        public Repository()
        {
            _storageFile = GetTestStorageFile().Result;
        }
        public Repository(StorageFile storageFile)
        {
            _storageFile = storageFile;
        }

        #region FullName  =====================================================
        public string FullName => _storageFile.Path;
        public string Name
        {
            get
            {
                var name = _storageFile.Name;
                var index = name.LastIndexOf(".");
                if (index < 0) return name;
                return name.Substring(0, index);
            }
        }
        #endregion

        #region FileFormat  ===================================================
        static Guid _fileFormat_1 = new Guid("D8CA7983-98BC-49CC-B821-432BDA6BADE6");
        #endregion

        #region Mark  =========================================================
        private enum Mark : byte
        {
            TableXBegin = 3,
            ColumnXBegin = 7,
            RelationXBegin = 10,
            RelationLinkBegin = 12,

            TableXEnding = 63,
            ColumnXEnding = 67,
            RelationXEnding = 70,
            RelationLinkEnding = 72,

            StorageFileEnding = 99,
        }
        #endregion

        #region Flags  ========================================================
        // don't read/write missing or default-value propties
        // these flags indicate which properties were non-default
        const byte BZ = 0;
        const byte B1 = 0x1;
        const byte B2 = 0x2;
        const byte B3 = 0x4;
        const byte B4 = 0x8;
        const byte B5 = 0x10;
        const byte B6 = 0x20;
        const byte B7 = 0x40;
        const byte B8 = 0x80;

        const ushort SZ = 0;
        const ushort S1 = 0x1;
        const ushort S2 = 0x2;
        const ushort S3 = 0x4;
        const ushort S4 = 0x8;
        const ushort S5 = 0x10;
        const ushort S6 = 0x20;
        const ushort S7 = 0x40;
        const ushort S8 = 0x80;
        const ushort S9 = 0x100;
        const ushort S10 = 0x200;
        const ushort S11 = 0x400;
        const ushort S12 = 0x800;
        const ushort S13 = 0x1000;
        const ushort S14 = 0x2000;
        const ushort S15 = 0x4000;
        const ushort S16 = 0x8000;
        #endregion

        #region TestStorageFile  ==============================================
        public static async Task<StorageFile> GetTestStorageFile()
        {
            return await ApplicationData.Current.LocalFolder.CreateFileAsync("NodeRepositorTest.nmdf", CreationCollisionOption.OpenIfExists);
        }
        #endregion

    }
}
