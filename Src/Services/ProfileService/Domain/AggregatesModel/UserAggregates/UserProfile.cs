using System;
using System.Globalization;
using System.Net;
using System.Net.Http.Headers;
using ShareRecipe.Services.Common.Domain;
using ShareRecipe.Services.ProfileService.Domain.AggregatesModel.UserAggregates.Events;

namespace ShareRecipe.Services.ProfileService.Domain.AggregatesModel.UserAggregates
{
    public class UserProfile : Entity, IAggregateRoot
    {
        public string Description { get; private set; }
        public string Image { get; private set; }

        public UserProfile(Guid id, string description, string image)
        {
            AddDomainEvent(new UserProfileCreatedDomainEvent(id));
            SetDescription(id, description);
            SetImage(id, image);
        }

        public void SetDescription(Guid id, string description)
        {
            if (description.Length > 300)
                throw new ArgumentException("Length of the description exceeds 300 characters.");
            Description = description;
            AddDomainEvent(new UserProfileDescriptionUpdateDomainEvent(id, description));
        }

        public void SetImage(Guid id, string image)
        {
            if (string.IsNullOrWhiteSpace(image))
                throw new ArgumentException("The image is null, empty or contains a whitespace.");
            if (IsImageUrl(image))
                throw new ArgumentException("The image url does not contain an image.");
            Image = image;
            AddDomainEvent(new UserProfileImageUpdateDomainEvent(id, image));
        }

        /// <summary>
        /// Validates the image url without downloading the image.
        /// </summary>
        /// <param name="image"></param>
        /// <returns></returns>
        private bool IsImageUrl(string image)
        {
            var req = (HttpWebRequest) HttpWebRequest.Create(image);
            req.Method = "HEAD";
            using var resp = req.GetResponse();
            return resp.ContentType.ToLower(CultureInfo.InvariantCulture)
                .StartsWith("image/");
        }
    }
}