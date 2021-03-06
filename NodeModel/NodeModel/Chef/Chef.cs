﻿using System;
using System.Collections.Generic;
using System.Text;

namespace NodeModel
{
    public partial class Chef : StoreOf<Store>
    {
        static int _newChefCount;
        private int _newChefNumber;

        #region Constructor  ==================================================
        public Chef(IRepository repository = null) : base(null, Trait.DataChef, 0)
        {
            Initialize();

            CheckRepository(repository);
        }
        #endregion

        #region Initialize  ===================================================
        private void Initialize()
        {
            InitializeStores();
            InitializeRelations();
            InitializeProperties();
        }
        #endregion

    }
}
