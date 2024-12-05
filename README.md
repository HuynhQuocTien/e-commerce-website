WEBSITE BÁN HÀNG sử dụng .NET CORE WEB API & REACT JS

1. Cài đặt source code:

- Server: .NET Core (port: 5001)
  +Cách 1: Sử dụng code first

        - Xóa toàn bộ các file migrations trong folder Migrations để tạo Database mới.

        - Vào appsetting để sửa lại đường dẫn kết nối (đường dẫn, tên database, ...) : connection string.

        - cmd project: chạy lệnh code first Migrations: dotnet ef migrations add initdatabase

        - lúc này bên folder Migration sẽ tạo ra 1 file initdatabase để mô tả các thành phần của database.

        - trong file này: ở table Reply - ta sửa ở Delete - onCascade thành NoAction.

        - tiếp theo: ta quay lại cmd và chạy lệnh: dotnet ef database update.

+Cách 2: Sử dụng database có sẵn

    - Vào appsetting để sửa lại đường dẫn kết nối (đường dẫn, tên database, ...) : DevConnection.

    - Sử dụng sqlserver 222, tạo database có tên trùng tên databse trên file appsetting.

    - Trên database vừa tạo click chuột phải =>Tasks =>Restore =>Database =>Device => Add => chọn database có sẵn => Chọn Ok.

- Client Trang chủ: React JS (port 3000)

  - run: npm start.

- Client Trang Quản Lý: React JS (port 8000)

  - run: npm start. (ở server khi tạo database đã seed data: tạo 1 tài khoản admin)

                    - username: admin
                    - password: 1234567890
