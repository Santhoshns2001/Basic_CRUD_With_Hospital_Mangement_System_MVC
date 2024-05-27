using CommonLayer.Model;
using Microsoft.Extensions.Configuration;
using Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Services
{
    public class PatientRepo :IPatientRepo
    {

       private readonly SqlConnection sqlConnection = new SqlConnection();

        private readonly string sqlConnectionString;

        private readonly IConfiguration configuration;

        public PatientRepo(IConfiguration configuration)
        {
            this.configuration = configuration;
            sqlConnectionString = configuration.GetConnectionString("DBConnection");
            sqlConnection.ConnectionString= sqlConnectionString;
        }


        public bool RegisterPatient(PatientModel patient)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("usp_RegisterPatient", sqlConnection);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@fullName", patient.FullName);
                cmd.Parameters.AddWithValue("@email", patient.Email);
                cmd.Parameters.AddWithValue("@contact", patient.Contact);
                cmd.Parameters.AddWithValue("@address", patient.Address);
                cmd.Parameters.AddWithValue("@dob", patient.DOB);
                cmd.Parameters.AddWithValue("@gender", patient.Gender);
                cmd.Parameters.AddWithValue("@patientImage", patient.PatientImage);

                sqlConnection.Open();
                cmd.ExecuteNonQuery();
                return true;

            }catch(Exception ex)
            {
                throw ex;
            }
            finally
            {
                sqlConnection.Close();
            }
            return false;
        }

        public List<PatientModel> GetAllPatient()
        {
            List<PatientModel> patients = new List<PatientModel>();

            try
            {
                if (sqlConnection != null)
                {
                    SqlCommand cmd = new SqlCommand("usp_FetchAllPatients", sqlConnection);
                    cmd.CommandType = CommandType.StoredProcedure;
                    sqlConnection.Open();
                    SqlDataReader sqlDataReader = cmd.ExecuteReader();
                    while (sqlDataReader.Read())
                    {
                        PatientModel patientModel = new PatientModel()
                        {
                            FullName = (string)sqlDataReader["FullName"],



                        };




                    }
                }

            }




        }
    }
}
