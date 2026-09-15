using Licencia.Modelos;
using System;
using System.Collections.Generic;

namespace Stockeate.Licensing
{ 
   public class LicenseInfo
   {
       public string Cliente { get; set; } = "";
  
       public string Empresa { get; set; } = "";
  
       public Guid InstallationId { get; set; }
  
       public LicenseType Tipo { get; set; }
  
       public DateTime FechaInicio { get; set; }
  
       public DateTime? FechaVencimiento { get; set; }
  
       public string Firma { get; set; } = "";
   }
} 