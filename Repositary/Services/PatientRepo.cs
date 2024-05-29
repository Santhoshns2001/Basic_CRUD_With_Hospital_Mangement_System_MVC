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

        public List<PatientModel> GetAllPatients()
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
                            PatientId = (int)sqlDataReader["PatientId"],
                            FullName = (string)sqlDataReader["FullName"],
                            Email = (string)sqlDataReader["Email"],
                            Contact = (long)sqlDataReader["Contact"],
                            Address = (string)sqlDataReader["Address"],
                            DOB = (DateTime)sqlDataReader["DOB"],
                            Age = (int)sqlDataReader["Age"],
                            Gender = (string)sqlDataReader["Gender"],
                            PatientImage = (string)sqlDataReader["PatientImage"],
                            IsTrash = (bool)sqlDataReader["IsTrash"],
                            CreatedAt = (DateTime)sqlDataReader["CreatedAt"],
                            UpdatedAt = (DateTime)sqlDataReader["UpdatedAt"]
                        };
                        patients.Add(patientModel);
                    }
                    return patients.ToList();
                }
                return null;

            }
            catch (Exception e)
            {
                throw e;

            }
            finally
            {
                sqlConnection.Close();

            }




        }

        public PatientModel GetPatientById(int patientId)
        {
            PatientModel patient = null;
            try
            {
                if (sqlConnection != null)
                {
                    SqlCommand cmd = new SqlCommand("usp_FetchByPatientId", sqlConnection);
                    cmd.CommandType= CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@patientid", patientId);
                    sqlConnection.Open();
                    SqlDataReader reader= cmd.ExecuteReader();
                    while (reader.Read())
                    {
                         patient = new PatientModel()
                        {
                            PatientId=(int)reader["PatientId"],
                            FullName = (string)reader["FullName"],
                            Email = (string)reader["Email"],
                            Contact = (long)reader["Contact"],
                            Address = (string)reader["Address"],
                            DOB = (DateTime)reader["DOB"],
                            Age = (int)reader["Age"],
                            Gender = (string)reader["Gender"],
                            PatientImage = (string)reader["PatientImage"],
                            IsTrash = (bool)reader["IsTrash"],
                            CreatedAt = (DateTime)reader["CreatedAt"],
                            UpdatedAt = (DateTime)reader["UpdatedAt"]
                        };
                    }
                    return patient;
                }
                else
                {
                    return null;
                }
            }catch(Exception ex)
            {
                throw ex;
            }
        }

      public  bool UpdatePatient(PatientModel patient)
        {
            try
            {
                if (sqlConnection != null)
                {
                    SqlCommand cmd = new SqlCommand("usp_UpdatePatient", sqlConnection);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@patientid", patient.PatientId);
                    cmd.Parameters.AddWithValue("fullName", patient.FullName);
                    cmd.Parameters.AddWithValue("@email", patient.Email);
                    cmd.Parameters.AddWithValue("@contact", patient.Contact);
                    cmd.Parameters.AddWithValue("address", patient.Address);
                    cmd.Parameters.AddWithValue("@dob", patient.DOB);
                    cmd.Parameters.AddWithValue("@gender", patient.Gender);
                    cmd.Parameters.AddWithValue("patientImage", patient.PatientImage);
                    sqlConnection.Open();
                    cmd.ExecuteNonQuery();
                    return true;
                }
                else
                {
                    throw new Exception("connection is not estblished properly");
                    return false; 
                }




            }catch(Exception ex)
            {
                throw ex;
            }
            finally
            {
                sqlConnection.Close();
            }
        }

       public bool DeletePatientConfirmed(int patientId)
        {
            try
            {
                if(sqlConnection != null)
                {
                    SqlCommand cmd = new SqlCommand("usp_DeletePatient", sqlConnection);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@patientid",patientId);
                    sqlConnection.Open();
                    cmd.ExecuteNonQuery();
                    return true;
                }
                else
                {
                    throw new Exception("connection is not established ");
                }
            }catch(Exception ex)
            {
                throw ex;
            }
            finally
            {
                sqlConnection.Close();
            }
        }
    }
}
