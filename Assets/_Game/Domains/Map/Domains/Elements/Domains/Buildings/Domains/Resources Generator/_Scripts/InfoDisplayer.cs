using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Game.Map.Element.Building.Resources
{
    public class InfoDisplayer : IInfoDisplayer
    {
        [Inject] private readonly ILevelManager _levelManager;
        [Inject] private readonly ResourceGeneratorLevelsData _levelsData;
        [Inject] private readonly UIResourceInfoDisplay _prefab;

        public void Display()
        {
            var currentData = _levelsData[_levelManager.CurrentLevel];
            GameObject.Instantiate(_prefab).Display(currentData);
        }

        //public List<InfoData> GetInfo()
        //{
        //    var dataList = new List<InfoData>();
        //    var currentData = _levelsData[_levelManager.CurrentLevel];

        //    var amountPerTimeRow = new InfoData()
        //    {
        //        Sprite = _levelsData.Resource.Sprite,
        //        Text = $"Production Rate: {currentData.AmountPerTime} per {currentData.TimeInMinute} mintues",
        //    };
        //    var capacity = new InfoData()
        //    {
        //        Sprite = _levelsData.Resource.Sprite,
        //        Text = $"Storage Capcity: {_levelsData[_levelsData.DataLevels.Count - 1].AmountPerTime}",
        //    };

        //    dataList.Add(amountPerTimeRow);
        //    dataList.Add(capacity);

        //    return dataList;
        //}
    }

}
