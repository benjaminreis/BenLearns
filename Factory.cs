using System;
namespace BenLearns
{
    public class Factory
    {
        public Factory()
        {
        }

        private BenLearns.Managers.Algorithms _Algorithms { get; set; }

        public BenLearns.Managers.Algorithms Algorithms 
        {
            get 
            {
                if(_Algorithms == null)
                {
                    _Algorithms = new BenLearns.Managers.Algorithms();
                }
                return _Algorithms;

            }
            set
            {
                _Algorithms = value;
            }

        }


        private BenLearns.Managers.HomeManager _HomeManager { get; set; }

        public BenLearns.Managers.HomeManager HomeManager
        {
            get
            {
                if (_HomeManager == null)
                {
                    _HomeManager = new BenLearns.Managers.HomeManager();
                }
                return _HomeManager;

            }
            set
            {
                _HomeManager = value;
            }

        }
    }
}
