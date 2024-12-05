# Website Bán Hàng

## Giới Thiệu

Dự án Website Bán Hàng là một ứng dụng web cho phépngười dùng thực hiện các chức năng mua sắm trực tuyến và quản lý hệ thống bán hàng. Dự án được xây dựng bằng:

- **Back-end**: .NET Core Web API
- **Front-end**: React JS

---

## Cấu Trúc Dự Án

### **1. Server (Back-end): .NET Core**

- **Port**: 5001
- **Framework**: .NET Core 8.0
- **Cơ sở dữ liệu**: SQL Server
- **Các chức năng chính**:
  - Quản lý người dùng (CRUD)
  - Quản lý sản phẩm (CRUD)
  - Quản lý đơn hàng (CRUD)
  - Tích hợp API xác thực người dùng
  - Seed dữ liệu tài khoản admin mặc định

### **2. Client (Front-end): React JS**

- **Port**:
  - **Trang chủ**: 3000
  - **Trang quản lý**: 8000
- **Framework**: React JS
- **Thư viện hỗ trợ**: React Router, Axios, Redux
- **Các chức năng chính**:
  - Trang chủ: Hiển thị sản phẩm, tìm kiếm, thêm sản phẩm vào giỏ hàng
  - Trang quản lý: Quản lý người dùng, sản phẩm, và đơn hàng
  - Hỗ trợ phân quyền admin và người dùng

---

## Hướng Dẫn Cài Đặt

### **1. Cài đặt Server**

#### **Cách 1: Sử dụng Code First**

1.  Xóa thư mục `Migrations` để tạo mới database.
2.  Mở file `appsettings.json` và chỉnh sửa chuỗi kết nối `ConnectionString`.
3.  Chạy lệnh sau để tạo Migration:
    ```bash
    dotnet ef migrations add initdatabase
    ```
4.  Lưu ý.

        - Bên folder Migration sẽ tạo ra 1 file initdatabase để mô tả các thành phần của database.

        - Trong file này: ở table Reply - ta sửa ở Delete - onCascade thành NoAction.

        - Tiếp theo: ta quay lại cmd và chạy lệnh: dotnet ef database update.

#### **Cách 2: Sử dụng database có sẵn**

1. Vào appsetting để sửa lại đường dẫn kết nối (đường dẫn, tên database, ...) : DevConnection.

2. Sử dụng sqlserver 2022, tạo database có tên trùng tên databse trên file appsetting.

3. Trên database vừa tạo click chuột phải =>Tasks =>Restore =>Database =>Device => Add => chọn database có sẵn => Chọn Ok.

## Client

### **1. Trang chủ**

- **Port**: 3000
- **Chức năng**: Hiển thị sản phẩm, tìm kiếm, thêm sản phẩm vào giỏ hàng, thanh toán.
- **Cách chạy**:
  1. Điều hướng đến thư mục React JS của trang chủ `.e-commerce-website\Frontend\client`
  2. Chạy các lệnh sau:
     ```bash
     npm install
     npm start
     ```
  3. Mở trình duyệt và truy cập `http://localhost:3000`.

---

### **2. Trang Quản Lý**

- **Port**: 8000
- **Chức năng**: Quản lý sản phẩm, người dùng, và đơn hàng.
- **Cách chạy**:

  1. Điều hướng đến thư mục React JS của trang quản lý `.\e-commerce-website\Frontend\admin` .
  2. Chạy các lệnh sau:
     ```bash
     npm install
     npm start
     ```
  3. Mở trình duyệt và truy cập `http://localhost:8000`.

- **Tài khoản admin mặc định** (đã seed sẵn trong database khi tạo):
  - **Username**: `admin`
  - **Password**: `1234567890`

---

**Lưu ý**: Nếu bạn gặp vấn đề hoặc cần hỗ trợ, vui lòng liên hệ qua email: `quoctien01062003@gmail.com`.
