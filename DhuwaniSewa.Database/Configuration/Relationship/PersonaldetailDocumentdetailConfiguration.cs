using DhuwaniSewa.Model.DbEntities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace DhuwaniSewa.Database.Configuration
{
    public class PersonaldetailDocumentdetailConfiguration : IEntityTypeConfiguration<PersonalDetailDocumentDetail>
    {
        public void Configure(EntityTypeBuilder<PersonalDetailDocumentDetail> builder)
        {
            builder.HasKey(a => new { a.PersonDetailId, a.DocumentDetailId });

            builder.HasOne<PersonalDetail>(a => a.PersonalDetail).
                WithMany(b => b.PersonalDetailDocumentDetails).
                HasForeignKey(c => c.PersonDetailId).
                HasConstraintName("FK_PersonalDetail_DocumenDetail").
                IsRequired();

            builder.HasOne<DocumentDetail>(a => a.DocumentDetail).
                WithMany(b => b.PersonalDetailDocumentDetails).
                HasForeignKey(c => c.DocumentDetailId).
                HasConstraintName("FK_DocumentDetail_PersonalDetailDocumentDetail").
                IsRequired();
        }
    }
}
