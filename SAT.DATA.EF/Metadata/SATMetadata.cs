using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SAT.DATA.EF//.Metadata
{
    #region Student Metadata
    public class StudentMetadata
    {
        //public int StudentId { get; set; }
        [Required(ErrorMessage = "*")]
        [StringLength(20, ErrorMessage = "* First Name cannot be more than 20 characters.")]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "*")]
        [StringLength(20, ErrorMessage = "* Last Name cannot be more than 20 characters.")]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [StringLength(15, ErrorMessage = "* Major cannot be more than 15 characters.")]
        [DisplayFormat(NullDisplayText ="N/A")]
        public string Major { get; set; }

        [StringLength(50, ErrorMessage = "* Address cannot be more than 50 characters.")]
        [DisplayFormat(NullDisplayText = "N/A")]
        public string Address { get; set; }

        [StringLength(25, ErrorMessage = "* City cannot be more than 25 characters.")]
        [DisplayFormat(NullDisplayText = "N/A")]
        public string City { get; set; }

        [StringLength(2, ErrorMessage = "* State cannot be more than 2 characters.")]
        [DisplayFormat(NullDisplayText = "N/A")]
        public string State { get; set; }

        [StringLength(10, ErrorMessage = "* Zip Code cannot be more than 10 characters.")]
        [DisplayFormat(NullDisplayText = "N/A")]
        [Display(Name = "Zip Code")]
        public string ZipCode { get; set; }

        [StringLength(13, ErrorMessage = "* Phone number cannot be more than 13 characters.")]
        [DisplayFormat(NullDisplayText = "N/A")]
        public string Phone { get; set; }

        [Required(ErrorMessage ="*")]
        [StringLength(60, ErrorMessage = "* Email cannot be more than 60 characters.")]
        public string Email { get; set; }

        [StringLength(100, ErrorMessage = "* Photo URL cannot be more than 100 characters.")]
        [DisplayFormat(NullDisplayText = "N/A")]
        [Display(Name = "Photo")]
        public string PhotoUrl { get; set; }

        [Required(ErrorMessage = "*")]
        public int SSID { get; set; }
    }
    [MetadataType(typeof(StudentMetadata))]
    public partial class Student {
        public static object ID { get; set; }

        [Display(Name = "Student")]
        public string FullName
        {
            get { return FirstName + " " + LastName; }
        }
    }
    #endregion


    #region Student Status Metadata
    public class StudentStatusMetadata
    {
        //public int SSID { get; set; }
        [Required(ErrorMessage ="*")]
        [StringLength(30, ErrorMessage = "* Student Status Name cannot be more than 30 characters.")]
        [Display(Name = "Student Status")]       
        public string SSName { get; set; }

        [UIHint("MultilineText")]
        [DisplayFormat(NullDisplayText = "N/A")]
        [StringLength(250, ErrorMessage = "* Description cannot be more than 250 characters.")]
        [Display(Name = "Description")]
        public string SSDescription { get; set; }
    }
    [MetadataType(typeof(StudentStatusMetadata))]
    public partial class StudentStatus { }
    #endregion


    #region Scheduled Class Status Metadata
    public class ScheduledClassStatusMetadata
    {
        //public int SCSID { get; set; }
        [Required(ErrorMessage = "*")]
        [StringLength(50, ErrorMessage = "* Scheduled Class Status name cannot be more than 50 characters.")]
        [Display(Name = "Scheduled Class Status")]
        public string SCName { get; set; }
    }
    [MetadataType(typeof(ScheduledClassStatusMetadata))]
    public partial class ScheduledClassStatus {
  
    }
    #endregion


    #region Scheduled Class Metadata
    public class ScheduledClassMetadata
    {
        //public int ScheduledClassId { get; set; }
        [Required(ErrorMessage ="*")]
        public int CourseId { get; set; }

        [Required(ErrorMessage ="*")]
        [DisplayFormat(DataFormatString = "{0:d}", ApplyFormatInEditMode = true)]
        [Display(Name = "Start Date")]
        public System.DateTime StartDate { get; set; }

        [Required(ErrorMessage = "*")]
        [DisplayFormat(DataFormatString = "{0:d}", ApplyFormatInEditMode = true)]
        [Display(Name = "End Date")]
        public System.DateTime EndDate { get; set; }

        [Required(ErrorMessage = "*")]
        [StringLength(40, ErrorMessage = "Instructor Name cannot be more than 40 characters.")]
        [Display(Name = "Instructor Name")]
        public string InstructorName { get; set; }

        [Required(ErrorMessage = "*")]
        [StringLength(20, ErrorMessage = "Location name cannot be more than 20 characters.")]
        public string Location { get; set; }

        [Required(ErrorMessage = "*")]
        public int SCISD { get; set; }
    }
    [MetadataType(typeof(ScheduledClassMetadata))]
    public partial class ScheduledClass
    {
        [Display(Name = "Class Summary")]
        public string ClassSummary
        {
            get { return $"{StartDate:d}" + " | " + Cours.CourseName + " | " + Location; }
        }
    }
    #endregion


    #region Enrollment Metadata
    public class EnrollmentMetadata
    {
        //public int EnrollmentId { get; set; }
        [Required(ErrorMessage = "*")]
        public int StudentId { get; set; }

        [Required(ErrorMessage = "*")]
        public int ScheduledClassId { get; set; }

        [Required(ErrorMessage = "*")]
        [DisplayFormat(DataFormatString = "{0:d}", ApplyFormatInEditMode = true)]
        [Display(Name = "Enrollment Date")]
        public System.DateTime EnrollmentDate { get; set; }
    }

    [MetadataType(typeof(EnrollmentMetadata))]
    public partial class Enrollment { }
    #endregion


    #region Course Metadata
    public class CourseMetadata
    {
        //public int CourseId { get; set; }
        [Required(ErrorMessage = "*")]
        [StringLength(50, ErrorMessage = "Course Name cannot be more than 50 characters.")]
        [Display(Name = "Course Name")]
        public string CourseName { get; set; }

        [Required(ErrorMessage = "*")]
        [UIHint("MultilineText")]
        [Display(Name = "Course Description")]
        public string CourseDescription { get; set; }

        [Required(ErrorMessage="*")]
        [Range(0, byte.MaxValue, ErrorMessage = "* Value must be a valid number, 0 or more.")]
        [DisplayFormat(NullDisplayText = "N/A")]
        [Display(Name = "Credit Hours")]
        public byte CreditHours { get; set; }

        [UIHint("MultilineText")]
        [DisplayFormat(NullDisplayText = "N/A")]
        [StringLength(250, ErrorMessage = "* Curriculum cannot be more than 250 characters.")]
        public string Curriculum { get; set; }

        [UIHint("MultilineText")]
        [DisplayFormat(NullDisplayText = "N/A")]
        [StringLength(500, ErrorMessage = "* Notes cannot be more than 500 characters.")]
        public string Notes { get; set; }

        [Required(ErrorMessage ="*")]
        [Display(Name = "Active")]
        public bool IsActive { get; set; }
    }

    [MetadataType(typeof(CourseMetadata))]
    public partial class Course { }
    #endregion
}
