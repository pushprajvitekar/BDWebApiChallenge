using Domain.Courses;
using Newtonsoft.Json;
using RestSharp;

namespace CategoryProvider
{
    public class CourseCategoryRepository : ICourseCategoryRepository
    {
        private readonly string apiUrl;
        public CourseCategoryRepository(string apiUrl)
        {
            this.apiUrl = apiUrl;
        }

        public IEnumerable<CourseCategory> GetCategories()
        {
            try
            {
                var options = new RestClientOptions(apiUrl)
                {
                    FailOnDeserializationError = true
                };
                var client = new RestClient(options);
                var response = client.ExecuteGet(new RestRequest($"CourseCategories"));
                if (response?.IsSuccessful == true && !string.IsNullOrEmpty(response.Content))
                {
                    var jsonlst = JsonConvert.DeserializeObject<List<Category>>(response.Content);
                    if (jsonlst != null)
                    {
                        return jsonlst.Select(json => new CourseCategory(json.id, json.name));
                    }
                    return new List<CourseCategory>();
                }
                return new List<CourseCategory>();
            }
            catch (Exception)
            {
                return new List<CourseCategory>();
            }
        }

        public CourseCategory? GetCategory(int id)
        {
            try
            {
                var options = new RestClientOptions(apiUrl)
                {
                    FailOnDeserializationError = true
                };
                var client = new RestClient(options);
                var response = client.ExecuteGet(new RestRequest($"CourseCategories?id={id}"));
                if ( response?.IsSuccessful== true && !string.IsNullOrEmpty(response.Content))
                {
                    var jsonlst = JsonConvert.DeserializeObject<List<Category>>(response.Content);
                    var json = jsonlst?.FirstOrDefault();
                    if (json != null)
                    {
                        return new CourseCategory(json.id, json.name);
                    }
                    return null;
                }
                return null;
            }
            catch (Exception)
            {
                return null;
            }


        }
    }
}
