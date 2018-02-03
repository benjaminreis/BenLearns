﻿using System;
using System.Linq;


namespace BenLearns.Managers
{
    public class HomeManager
    {
        public HomeManager()
        {
        }

        private BenLearns.Factory _Factory { get; set; }
        public BenLearns.Factory Factory
        {
            get
            {
                if (_Factory == null)
                {
                    _Factory = new BenLearns.Factory();
                }
                return _Factory;
            }
            set
            {
                _Factory = value;
            }
        }



        internal void ComputeMeanMedianMode(ref BenLearns.ViewModels.HackerRankViewModel model)
        {
            model.CountElements = int.Parse(model.sCountElements);
            model.Elements = model.sElements.Split(',').Select(int.Parse).ToList();

            string Mean = Factory.Algorithms.ComputeMean(model.Elements, model.CountElements).ToString();
            string Median = Factory.Algorithms.ComputeMedian(model.Elements, model.CountElements).ToString();
            string Mode = Factory.Algorithms.ComputeMode(model.Elements, model.CountElements).ToString();

            model.sResult = $"Mean: {Mean}  Median: {Median}  Mode: {Mode}";


        }
    }
}