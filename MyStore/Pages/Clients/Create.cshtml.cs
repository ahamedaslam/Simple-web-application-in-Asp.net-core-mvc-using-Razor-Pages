using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Hosting;
using System.Data.SqlClient;

namespace MyStore.Pages.Clients
{
    public class CreateModel : PageModel
    {
        public Clientinfo Clientinfo = new Clientinfo();

        public string errorMessage = "";
        public string successMessage = "";
        public void OnGet()
        {

        }

        public void OnPost()
        {

            //Clientinfo = new Clientinfo();: This line creates a new instance
            Clientinfo = new Clientinfo();

            //Clientinfo.name = Request.Form["name"];: This line retrieves the value of the
            //"name"
            //field from the HTTP POST request's form data using the
            //Request.Form collection and assigns it to the name
            //property of the Clientinfo object




            Clientinfo.name = Request.Form["name"];
            Clientinfo.email = Request.Form["email"];
            Clientinfo.phone = Request.Form["phone"];
            Clientinfo.address = Request.Form["address"];
            Clientinfo.client_Type = Request.Form["client_Type"];

            if (Clientinfo.name.Length == 0 || Clientinfo.email.Length == 0
                || Clientinfo.phone.Length == 0 || Clientinfo.address.Length == 0 || Clientinfo.client_Type.Length == 0) 
            {
                errorMessage = "All the fields are required";
                return;
            }




            try
            {
                string connectionString = "Data Source=MY-PC;Initial Catalog=mystore;Integrated Security=True";
                using (SqlConnection connection = new SqlConnection(connectionString)) 
                { 
                //open the connection
                       connection.Open();
                       string sql = "INSERT INTO clients " + " (name,email,phone,address,client_Type)VALUES " + " (@name,@email,@phone,@address,@client_Type); ";


                    using (SqlCommand command = new SqlCommand(sql, connection))

                    {
                        command.Parameters.AddWithValue("@name", Clientinfo.name);
                        command.Parameters.AddWithValue("@email", Clientinfo.email);
                        command.Parameters.AddWithValue("@phone", Clientinfo.phone);
                        command.Parameters.AddWithValue("@address", Clientinfo.address);
                        command.Parameters.AddWithValue("@client_type", Clientinfo.client_Type);



                        command.ExecuteNonQuery();
                        successMessage = "New client added successfully";
                    }
                }
            }

            catch (Exception ex) 
            { 
              errorMessage = ex.Message;
                return;
            }

            // TODO: Implement database interaction to save the new client
            // Example using Entity Framework:
            // dbContext.Clients.Add(Clientinfo);
            // dbContext.SaveChanges();

           

            // Clear the form fields after successful submission (if needed)
            Clientinfo.name = "";
            Clientinfo.email = "";
            Clientinfo.phone = "";
            Clientinfo.address = "";
            Clientinfo.client_Type = "";

            

            Response.Redirect("/Clients/Index");
            
        }

    }
}