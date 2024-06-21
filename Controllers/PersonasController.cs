using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PruebaTecnicaAlmarchivos.Models;
using System.Data.SqlClient;
using System.Data;
using PruebaTecnicaAlmarchivos.Permisos;


namespace PruebaTecnicaAlmarchivos.Controllers
{
    [ValidarSesion]
    public class PersonasController : Controller
    {
        //CAMBIAR CADENA DE CONEXION PARA QUE FUNCIONE CUANDO SE DESCARGUE EL PROYECTO
        private static string cadena = "Data Source=FAMILIA\\SQLEXPRESS;Initial Catalog=PruebaAlmaArchivos;Integrated Security=True;Encrypt=False";
        private static List<Personas> olista = new List<Personas>();


        // GET: Personas
        public ActionResult Inicio()
        {
            olista = new List<Personas>();

            using (SqlConnection oconexion = new SqlConnection(cadena))
            {
                SqlCommand cmd = new SqlCommand("SELECT * FROM Personas", oconexion);
                cmd.CommandType = CommandType.Text;
                oconexion.Open();

                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {

                        Personas nuevoPersona = new Personas();

                        nuevoPersona.Id = Convert.ToInt32(dr["Id"]);
                        nuevoPersona.Nombres = dr["Nombres"].ToString();
                        nuevoPersona.Apellidos = dr["Apellidos"].ToString();
                        nuevoPersona.NumeroIdentificacion = dr["NumeroIdentificacion"].ToString();
                        nuevoPersona.Email = dr["Email"].ToString();
                        nuevoPersona.TipoIdentificacion = dr["Email"].ToString();
                        nuevoPersona.FechaCreacion = Convert.ToDateTime(dr["FechaCreacion"]);

                        olista.Add(nuevoPersona);

                    }
                }
            }
            return View(olista);
        }

        [HttpGet]
        public ActionResult Registrar()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Registrar(Personas oPersonas)
        {
            using (SqlConnection oconexion = new SqlConnection(cadena))
            {
                SqlCommand cmd = new SqlCommand("sp_Registrar", oconexion);
                cmd.Parameters.AddWithValue("Nombres", oPersonas.Nombres);
                cmd.Parameters.AddWithValue("Apellidos", oPersonas.Apellidos);
                cmd.Parameters.AddWithValue("NumeroIdentificacion", oPersonas.NumeroIdentificacion);
                cmd.Parameters.AddWithValue("Email", oPersonas.Email);
                cmd.Parameters.AddWithValue("TipoIdentificacion", oPersonas.TipoIdentificacion);
  
                cmd.CommandType = CommandType.StoredProcedure;
                oconexion.Open();
                cmd.ExecuteNonQuery();
            }
            return RedirectToAction("Inicio", "Personas");
        }


        [HttpGet]
        public ActionResult Editar(int? Id)
        {
            if (Id == null)
             
                return RedirectToAction("Inicio", "Personas");
            

            Personas oPersonas = olista.Where(c => c.Id == Id).FirstOrDefault();


            return View(oPersonas);
        }


        [HttpPost]
        public ActionResult Editar(Personas oPersonas)
        {
            using (SqlConnection oconexion = new SqlConnection(cadena))
            {
                SqlCommand cmd = new SqlCommand("sp_Editar", oconexion);
                cmd.Parameters.AddWithValue("Id", oPersonas.Id);
                cmd.Parameters.AddWithValue("Nombres", oPersonas.Nombres);
                cmd.Parameters.AddWithValue("Apellidos", oPersonas.Apellidos);
                cmd.Parameters.AddWithValue("NumeroIdentificacion", oPersonas.NumeroIdentificacion);
                cmd.Parameters.AddWithValue("Email", oPersonas.Email);
                cmd.Parameters.AddWithValue("TipoIdentificacion", oPersonas.TipoIdentificacion);
                cmd.CommandType = CommandType.StoredProcedure;
                oconexion.Open();
                cmd.ExecuteNonQuery();
            }
            return RedirectToAction("Inicio", "Personas");
        }


        [HttpGet]
        public ActionResult Eliminar(int? Id)
        {
            if (Id == null)
                return RedirectToAction("Inicio", "Personas");


            Personas ocontacto = olista.Where(c => c.Id == Id).FirstOrDefault();
            return View(ocontacto);
        }

        [HttpPost]
        public ActionResult Eliminar(string Id)
        {
            using (SqlConnection oconexion = new SqlConnection(cadena))
            {
                SqlCommand cmd = new SqlCommand("sp_Eliminar", oconexion);
                cmd.Parameters.AddWithValue("Id", Id);
                cmd.CommandType = CommandType.StoredProcedure;
                oconexion.Open();
                cmd.ExecuteNonQuery();
            }
            return RedirectToAction("Inicio", "Personas");
        }



    }
}