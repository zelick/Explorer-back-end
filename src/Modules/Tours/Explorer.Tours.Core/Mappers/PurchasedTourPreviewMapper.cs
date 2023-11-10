using Explorer.Tours.API.Dtos;
using Explorer.Tours.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Tours.Core.Mappers
{
    public class PurchasedTourPreviewMapper
    {
        private CheckpointMapper checkpointMapper;
        private EquipmentMapper equipmentMapper;
        private TourRatingPreviewMapper ratingMapper;
        private TourTimeMapper timeMapper;
        public PurchasedTourPreviewMapper()
        {
            checkpointMapper = new CheckpointMapper();
            equipmentMapper = new EquipmentMapper();
            ratingMapper = new TourRatingPreviewMapper();
            timeMapper = new TourTimeMapper();
        }

        public PurchasedTourPreviewDto createDto(PurchasedTourPreview purchasedTourPreview)
        {

            PurchasedTourPreviewDto result = new PurchasedTourPreviewDto();

            result.Name = purchasedTourPreview.Name;
            result.Id = purchasedTourPreview.Id;
            //result.AuthorId = tourPreview.AuthorId;
            result.Description = purchasedTourPreview.Description;
            result.Price = purchasedTourPreview.Price;
            result.DemandignessLevel = purchasedTourPreview.DemandignessLevel.ToString();
            result.Checkpoints = checkpointMapper.createListDto(purchasedTourPreview.Checkpoints);
            result.Tags = purchasedTourPreview.Tags;
            result.Equipment = equipmentMapper.createListDto(purchasedTourPreview.Equipment);
            result.TourRatings = ratingMapper.createListDto(purchasedTourPreview.TourRatings);
            result.TourTimes = timeMapper.createListDto(purchasedTourPreview.TourTimes);

            return result;
        }

        public List<PurchasedTourPreviewDto> createDtoList(List<PurchasedTourPreview>? purchasedTourPreviewList)
        {
            List<PurchasedTourPreviewDto> purchasedTourPreviewDtos = new List<PurchasedTourPreviewDto>();
            foreach (var purchasedTourPreview in purchasedTourPreviewList)
            {
                purchasedTourPreviewDtos.Add(createDto(purchasedTourPreview));
            }
            return purchasedTourPreviewDtos;
        }
    }
}
