# StudentManagementSystem

## Tổng Quan
Đây là repository cho ứng dụng .NET với ba dự án chính: server, client.blazor và shared. 
Ứng dụng sử dụng gRPC để giao tiếp giữa server và client Blazor.

### server
Chứa các dịch vụ gRPC được triển khai trong .NET, cùng với các repository để giao tiếp với cơ sở dữ liệu.

### client.blazor
Là ứng dụng web Blazor tương tác với người dùng và giao tiếp với server qua gRPC.

### client.consoleapp
Là ứng dụng console tương tác với người dùng và giao tiếp với server qua gRPC.

### shared
Chứa các contract gRPC.
