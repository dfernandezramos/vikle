using MvvmCross.Navigation;
using Vikle.Core.Models;

namespace Vikle.Core.ViewModels
{
    /// <summary>
    /// This class implements the reparation detail viewmodel for the client side
    /// </summary>
    public class ReparationDetailVM : ClientBaseVM<Reparation>
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
                RaiseAllPropertiesChanged();
                RaisePropertyChanged(() => Model);
            }
        }

        public ReparationDetailVM(IMvxNavigationService mvxNavigationService) : base(mvxNavigationService)
        {
        }

        public override void Prepare(Reparation reparation)
        {
            base.Prepare(reparation);

            Model = reparation;
        }
    }
}