using System;
using System.Collections.Generic;
using System.Text;

namespace Oryx.FastAdmin.Core.StateManage
{
    public class StateHub
    {
        private Dictionary<string, Action<StateModel>> StateHubDictionary = new Dictionary<string, Action<StateModel>>();

        public void SendState(StateModel stateModel)
        {
            if (StateHubDictionary.ContainsKey(stateModel.Name))
            {
                StateHubDictionary[stateModel.Name](stateModel);
            }
        }

        public void RecieveState(string stateName, Action<StateModel> stateHandler)
        {
            if (StateHubDictionary.ContainsKey(stateName))
            {
                StateHubDictionary.Add(stateName, stateHandler);
            }
        }
    }
}
