using System.ComponentModel.DataAnnotations;

public enum QualificationType
{
  [Display(Name = "Bachelor Degree")]
  BachelorDegree,
  [Display(Name = "Diploma")]
  Diploma,
  [Display(Name = "Advanced Diploma")]
  AdvancedDiploma,
  [Display(Name = "Bachelor Honors Degree")]
  BachelorDegreeWithHons,
  [Display(Name = "Masters Degree")]
  MastersDegree,
  [Display(Name = "PhD Degree")]
  PhD

}