using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TeachEquipManagement.DAL.Models;

namespace TeachEquipManagement.DAL.SeedData
{
    public class SupplierSeed : IEntityTypeConfiguration<Supplier>
    {
        public void Configure(EntityTypeBuilder<Supplier> builder)
        {
            builder.HasData
            (
                new Supplier
                {
                    Id = 1,
                    SupplierName = "Công ty Cổ Phần Tập Đoàn Hòa Phát",
                    ContactName = "Trần Đình Long",
                    Address = "171 Trường Chinh, P. An Khê, Q. Thanh Khê, Đà Nẵng",
                    Phone = "02363721232"
                },
                new Supplier
                {
                    Id = 2,
                    SupplierName = "Công ty Cổ Phần Tập Đoàn Hoa Sen",
                    ContactName = "Lê Phước Vũ",
                    Address = "Số 9, Đại lộ Thống Nhất, Khu công nghiệp Sóng Thần II, Phường Dĩ An, Thành phố Dĩ An, Tỉnh Bình Dương, Việt Nam.",
                    Phone = "18001515"
                },
                new Supplier
                {
                    Id = 3,
                    SupplierName = "Công ty Cổ Phần Tôn Đông Á",
                    ContactName = "Tôn Giả Hoàng",
                    Address = "Số 5, đường số 5, Khu Công Nghiệp Sóng Thần 1, phường Dĩ An, thành phố Dĩ An, tỉnh Bình Dương",
                    Phone = "02743790420"
                },
                new Supplier
                {
                    Id = 4,
                    SupplierName = "Công ty Cổ Phần Vicostone",
                    ContactName = "Hồ Xuân Năng",
                    Address = "Khu công nghệ cao Hòa Lạc, Thạch Hòa, Thạch Thất, Hà Nội",
                    Phone = "18006766"
                },
                new Supplier
                {
                    Id = 5,
                    SupplierName = "Tổng công ty Viglacera - CTCP",
                    ContactName = "Nguyễn Văn Tuấn",
                    Address = "Tòa nhà Viglacera, Số 1 Đại lộ Thăng Long, Hà Nội",
                    Phone = "35536660"
                },
                new Supplier
                {
                    Id = 6,
                    SupplierName = "Công ty Cổ Phần Eurowindow",
                    ContactName = "Tống Gia Phát",
                    Address = "Tòa nhà văn phòng Eurowindow Office Building, Số 02 Tôn Thất Tùng, Đống Đa, Hà Nội",
                    Phone = "37474777"
                },
                   new Supplier
                   {
                       Id = 7,
                       SupplierName = "Công ty Cổ Phần Công Nghiệp Vĩnh Tường",
                       ContactName = "Vĩnh Thế Tương",
                       Address = "Lô C23a, Khu Công Nghiệp Hiệp Phước, Xã Hiệp Phước, Huyện Nhà Bè, Thành phố Hồ Chí Minh, Việt Nam",
                       Phone = "0837818554"
                   }
            );
        }
    }
}
