using MvvmCross.Navigation;
using Vikle.Core.Models;

namespace Vikle.Core.ViewModels
{
    /// <summary>
    /// This class contains the implementation of the Reparation Detail VM for the Worker side
    /// </summary>
    public class WSReparationDetailVM : WorkerBaseVM<Reparation>
    {
        private Reparation _model;

        /// <summary>
        /// Gets or sets the vehicle data model.
        /// </summary>
        public Reparation Model
        {
            get => _model;
            set
            {
                _model = value;
                RaisePropertyChanged(() => Model);
            }
        }
        
        public WSReparationDetailVM(IMvxNavigationService mvxNavigationService) : base(mvxNavigationService)
        {
            
        }
        
        public override void Prepare(Reparation reparation)
        {
            base.Prepare(reparation);

            Model = reparation;
        }
    }
}