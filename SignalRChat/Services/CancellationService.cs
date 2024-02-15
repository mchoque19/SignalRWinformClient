using DAL.Models;
using DAL.Repositories;
using NuGet.ProjectModel;

namespace SignalRChat.Services
{
    public class CancellationService
    {
        private readonly CancellationRepository _cancellationRepo;

        public CancellationService(CancellationRepository cancellationRepo)
        {
            _cancellationRepo = cancellationRepo;
        }

        public Cancellation? NewCancellation(OrderItem orderItem, float units, bool transferOperation = false) 
        {
            if (orderItem.Units - units < 0)
            {
                return null;
            }
            else
            {
                Cancellation cancellation = new()
                {
                    OrderItem = orderItem,
                    Units = units,
                    Confirmed = transferOperation ? DateTime.Now : null
                };
                cancellation = _cancellationRepo.Save(cancellation);
                return cancellation;
            }

        }
    }
}
