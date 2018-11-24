﻿using ChromFXUI;
using Newtonsoft.Json;
using ReduxCore;

namespace ReduxStyleUI.XP
{
    public partial class ReduxStyleForm<State> : ChromFXBaseForm where State : struct,IState
    {
        private Package<State> store;
        public virtual Package<State> Store
        {
            private set
            {
                store = value;
            }
            get
            {
                return store;
            }
        }

        private string jsDispatchMethod = "vm.dispatch({0})";
        public string JsDispatchMethod
        {
            set { jsDispatchMethod = value; }
            get { return jsDispatchMethod; }
        }

        public ReduxStyleForm()
            :base(null)
        {

        }

        public ReduxStyleForm(Package<State> store, string initialUrl)
            :base(initialUrl)
        {
            Store = store;

            Store.Subscribe((subscription,action)=>
            {
                var state = store.GetState();
                string cmd = string.Format(jsDispatchMethod, JsonConvert.SerializeObject(state));
                ExecuteJavascript(cmd);
            });
        }

    }
}
