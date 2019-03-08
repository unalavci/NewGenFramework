﻿using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NewGenFramework.MvcWebUi.Models.ViewModels
{               
[Table("UnitTypes")]
[DisplayColumn("UnitTypeName")]
[DisplayName("UnitType")]
public class UnitTypeViewModel
{
[Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
[Display(Name = "UnitTypeId Id", AutoGenerateField = false)]
public virtual int UnitTypeId{ get; set; }
[Display(Name = "UnitTypeName"), Required()]
public virtual string UnitTypeName{ get; set; }

     }
}