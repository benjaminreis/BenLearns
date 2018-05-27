using System;
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
            if (!string.IsNullOrWhiteSpace(model.sElements))
            {
                //TODO BEN there is an issue with a "hanging" comma
                model.Elements = model.sElements.Split(',').Select(int.Parse).ToList();

                //TODO BEN need to validate the splitting of the string.
                model.CountElements = model.Elements.Count();

                string Mean = Factory.Algorithms.ComputeMean(model.Elements, model.CountElements).ToString();
                string Median = Factory.Algorithms.ComputeMedian(model.Elements, model.CountElements).ToString();
                string Mode = Factory.Algorithms.ComputeMode(model.Elements, model.CountElements).ToString();

                model.sResult = $"Mean: {Mean}  Median: {Median}  Mode: {Mode}";
            }
            else
            {
                model.sResult = "Please enter values";
            }


        }

        internal void ComputeStandardDeviation(ref BenLearns.ViewModels.HackerRankViewModel model)
        {
            if (!string.IsNullOrWhiteSpace(model.sElements))
            {
                //TODO BEN need to validate the splitting of the string.
                //TODO BEN there is an issue with a "hanging" comma

                model.Elements = model.sElements.Split(',').Select(int.Parse).ToList();
                model.CountElements = model.Elements.Count();

                var Mean = Factory.Algorithms.ComputeMean(model.Elements, model.CountElements);

                double SumMeans = new double();

                foreach (int num in model.Elements)
                {
                    SumMeans += Math.Pow((num - Mean), 2);
                }

                model.sResult = Math.Pow((SumMeans / model.CountElements), .5).ToString();

            }
        }

    }
}
