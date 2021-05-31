using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using ShareRecipe.Services.Common.Domain;

namespace ShareRecipe.Services.KweetService.Domain.AggregatesModel.KweetAggregates
{
    public class ProfileAggregate : Entity, IAggregateRoot
    {
        public List<Kweet> Kweets { get; private set; }
        public string DisplayName { get; private set; }
        public string ProfilePictureUrl { get; private set; }
        
        protected ProfileAggregate()
        {
            Kweets = new List<Kweet>();
        }

        public ProfileAggregate(Guid userid, string displayName, string profilePictureUrl)
        {
            SetId(userid);
            SetDisplayName(displayName);
            SetProfilePictureUrl(profilePictureUrl);
        }

        public Kweet CreateKweetAsync(string message)
        {
            List<string> directions = null;
            List<Ingredient> ingredients = null;
            using (StringReader sr = new StringReader(message)) {
                string line;
                while ((line = sr.ReadLine()) != null) {
                    if (line.StartsWith("-"))
                        ingredients.Add(new Ingredient(line, null));
                    if (Regex.IsMatch(line, @"^\d\."))
                        directions.Add(line);
                }
            }

            Kweet kweet = new(message, ingredients, directions);
            Kweets.Add(kweet);
            return kweet;
        }

        public void SetProfilePictureUrl(string image)
        {
            if (string.IsNullOrWhiteSpace(image))
                throw new ArgumentException("The image is null, empty or contains a whitespace.");
            ProfilePictureUrl = image;
        }

        public void SetDisplayName(string displayName)
        {
            if (string.IsNullOrWhiteSpace(displayName))
                throw new ArgumentException("Display name is empty.");
            if (displayName.Length > 30)
                throw new ArgumentException("The length of the display name has exceed the allowed characters");
            DisplayName = displayName ?? throw new ArgumentNullException(nameof(displayName));
        }
    }
}