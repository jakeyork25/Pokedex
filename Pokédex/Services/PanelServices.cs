using Pokédex.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace Pokédex.Services
{
    public class PanelServices
    {


        public string GetPanelForLeft(PanelModel model)
        {
            if (model.centerPanel == null) return "null";
            var panelList = model.panelList;
            var centerPanelIndex = panelList.IndexOf(model.centerPanel);
            var leftPanelIndex = centerPanelIndex - 3;
            if (leftPanelIndex < 0) leftPanelIndex += panelList.Count;
            return panelList.ElementAt(leftPanelIndex);
        }

        public string GetPanelForRight(PanelModel model)
        {
            if (model.centerPanel == null) return "null";
            var panelList = model.panelList;
            var centerPanelIndex = panelList.IndexOf(model.centerPanel);
            var rightPanelIndex = centerPanelIndex + 3;
            if (rightPanelIndex > panelList.Count - 1) rightPanelIndex -= panelList.Count;
            return panelList.ElementAt(rightPanelIndex);
        }

        public List<string> GetFireTypeList()
        {
            string[] panelArray = { "panel-1", "panel-2", "panel-3", "panel-4", "panel-5", "panel-6" };
            var panelList = panelArray.ToList();
            return panelList;
        }
    }
}
