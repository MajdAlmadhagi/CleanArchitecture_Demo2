using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CleanArchitecture.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class first_migration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)//Up>>دالة تتم عند الرفع سواء انشاء او تحديث
        {
            migrationBuilder.CreateTable(//الان يقوم بأنشاء جدول بنفس اسم المودل الذي حددناه في الDbContext ليتم رفعها لقاعدة البيانات او جلبها
                name: "Users",
                columns: table => new  //الاعمدة التي في المودل
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),//هنا يتم زيادة العداد كل مره بواحد تلقائيا
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),//nullable   اي غير قابله ان تكون فاضيه
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)//Down>>  دالة تتم عند محاولة حذف الجداول السابقه الذي تم انشاءها
        {
            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
