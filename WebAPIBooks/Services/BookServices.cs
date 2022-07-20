using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using WebAPIBooks.Models;

namespace WebAPIBooks.Services
{
    public class BookServices
    {
        private readonly string UrlBaseApi = "https://fakerestapi.azurewebsites.net/api/v1";

        public async Task<List<Books>> GetAllBooks()
        {
            var url = UrlBaseApi + "/Books";
            var result = await GethttClientReadAsString(url);

            var ListBooks   = JsonConvert.DeserializeObject<List<Books>>(result);
            return ListBooks;
        }

        public async Task<Books> GetBook(int id)
        {
            var url = UrlBaseApi + $"/Books/{id}";
            var result = await GethttClientReadAsString(url);

            var Books = JsonConvert.DeserializeObject<Books>(result);
            return Books;
        }

        public async Task<List<Authors>> GetAuthorsByBooks(int idBook)
        {
            var url = UrlBaseApi + $"/Authors/authors/books/{idBook}";
            var result = await GethttClientReadAsString(url);

            var Authors = JsonConvert.DeserializeObject<List<Authors>>(result);
            return Authors;
        }

        public async Task<Books> PostBook(Books book)
        {
            var url = UrlBaseApi + "/Books";
            using (var httpClient = new HttpClient())
            {
                using var response = await httpClient.PostAsJsonAsync(url, book);
                response.EnsureSuccessStatusCode();
                var result = await response.Content.ReadAsStringAsync();

                var Books = JsonConvert.DeserializeObject<Books>(result);
                return Books;
            }
        } 
        
        public async Task<Books> PutBook(int id, Books book)
        {
            var url = UrlBaseApi + $"/Books/{id}";
            using (var httpClient = new HttpClient())
            {
                using var response = await httpClient.PutAsJsonAsync(url, book);
                response.EnsureSuccessStatusCode();
                var result = await response.Content.ReadAsStringAsync();

                var Book = JsonConvert.DeserializeObject<Books>(result);
                return Book;
            }
        }
        
        public async Task<Books> DeleteBook(int id)
        {

            var url = UrlBaseApi + $"/Books/{id}";
            using (var httpClient = new HttpClient())
            {
                using var response = await httpClient.DeleteAsync(url);
                response.EnsureSuccessStatusCode();
                var result = await response.Content.ReadAsStringAsync();

                var Book = JsonConvert.DeserializeObject<Books>(result);
                return Book;
            }
        }


        //handle HttpCLient for ReadAsString
        private async Task<string> GethttClientReadAsString(string url) 
        {
            using (var httpClient = new HttpClient())
            {
                using var response = await httpClient.GetAsync(url);
                response.EnsureSuccessStatusCode();
                var result = await response.Content.ReadAsStringAsync();

                return result;
            }
        }

    }
}
