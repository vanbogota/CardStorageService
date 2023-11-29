using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace CardStorageService.DAL
{
    [Table("Accounts")]
    public class Account
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int AccountId { get; set; }

        [StringLength(100)]
        public string EMail { get; set; } = null!;

        [StringLength(100)]
        public string PasswordSalt { get; set; } = null!;

        [StringLength(100)]
        public string PasswordHash { get; set; } = null!;

        public bool Locked { get; set; }

        [StringLength(255)]
        public string FirstName { get; set; } = null!;

        [StringLength(255)]
        public string LastName { get; set; } = null!;

        [StringLength(255)]
        public string SecondName { get; set; } = null!;

        [InverseProperty(nameof(AccountSession.Account))]
        public virtual ICollection<AccountSession> Sessions { get; set; } = new HashSet<AccountSession>();

    }
}
