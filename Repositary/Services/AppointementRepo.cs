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
    public class AppointementRepo : IAppointmentRepo
    {
      private readonly  SqlConnection SqlConnection=new SqlConnection();
        private readonly string sqlConnectionString;
        private readonly IConfiguration configuration;

        public AppointementRepo(IConfiguration configuration)
        {
            this.configuration = configuration;
            sqlConnectionString = configuration.GetConnectionString("DBConnection");
            SqlConnection.ConnectionString= sqlConnectionString;
        }

       public   bool CreateAppointment(AppointmentModel appointmentmodel)
        {
            try
            {
                if (SqlConnection != null)
                {
                    SqlCommand cmd = new SqlCommand("usp_CreateAppointment", SqlConnection);
                    cmd.CommandType=CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@doctorid", appointmentmodel.Doctor_Id);
                    cmd.Parameters.AddWithValue("@patientid", appointmentmodel.PatientId);
                    cmd.Parameters.AddWithValue("@appointmentdate", appointmentmodel.AppointmentDate);
                    cmd.Parameters.AddWithValue("@starttime", appointmentmodel.StartTime);
                    cmd.Parameters.AddWithValue("@endtime", appointmentmodel.EndTime);
                    cmd.Parameters.AddWithValue("@concerns", appointmentmodel.Concerns);
                    SqlConnection.Open();
                    cmd.ExecuteNonQuery();
                    return true;
                }
                else
                {
                    throw new Exception("unable to build connection");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                SqlConnection.Close();
            }
        }

       public List<AppointmentModel> GetAllAppointments()
        {
           List<AppointmentModel> appointments = new List<AppointmentModel>();

            try
            {
                if (SqlConnection != null)
                {
                    SqlCommand cmd = new SqlCommand("usp_GetAllAppointments", SqlConnection);
                    cmd.CommandType = CommandType.StoredProcedure;
                    SqlConnection.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        AppointmentModel appointment = new AppointmentModel()
                        {
                            AppointmentId = (int)reader["AppointmentId"],
                            Doctor_Id = (int)reader["Doctor_Id"],
                            PatientId = (int)reader["PatientId"],
                            AppointmentDate = (DateTime)reader["AppointmentDate"],
                            StartTime = (DateTime)reader["StartTime"],
                            EndTime = (DateTime)reader["EndTime"],
                            Concerns = (string)reader["Concerns"]
                        };
                        appointments.Add(appointment);
                    }
                    return appointments;
                }
                else
                {
                    throw new Exception("connection was not established");
                }
            }
            catch (Exception ex) { throw ex; }
            finally {  SqlConnection.Close(); }
        }
       
      public  List<DoctorPatientModel> GetDoctorAndPatientProfiles()
        {
            List<DoctorPatientModel> models = new List<DoctorPatientModel>();

            try
            {
                if (SqlConnection != null)
                {
                    SqlCommand cmd = new SqlCommand("GetDoctorAndPatientProfiles", SqlConnection);
                    cmd.CommandType = CommandType.StoredProcedure;
                    SqlConnection.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        DoctorPatientModel doctorPatientModel = new DoctorPatientModel()
                        {
                            DoctorName = (string)reader["DoctorName"],
                            DoctorImage = (string)reader["DoctorImage"],
                            PatientId = (int)reader["PatientId"],
                            PatientName = (string)reader["PatientName"],
                            PatientEmail = (string)reader["PatientEmail"],
                            PatientContact = (long)reader["PatientContact"],
                            PatientAddress = (string)reader["PatientAddress"],
                            PatientDOB = (DateTime)reader["PatientDOB"],
                            PatientAge = (int)reader["PatientAge"],
                            PatientGender = (string)reader["PatientGender"],
                            PatientImage = (string)reader["PatientImage"],
                            PatientIsTrash = (bool)reader["PatientIsTrash"],
                            PatientCreatedAt = (DateTime)reader["PatientCreatedAt"],
                            PatientUpdatedAt = (DateTime)reader["PatientUpdatedAt"]

                        };
                        models.Add(doctorPatientModel);
                    }
                    return models.ToList();
                }
                else
                {
                    throw new Exception("connections was not established");
                }

            }catch(Exception ex)
            {
                throw ex;
            }
            finally
            {
                SqlConnection.Close();
            }
        }
    }
}
