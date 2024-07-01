CREATE DATABASE qlsv
GO

USE qlsv
GO

CREATE TABLE teacher
(
	teacher_id INT PRIMARY KEY IDENTITY(1, 1),
	teacher_fullname NVARCHAR(255) NOT NULL,
	teacher_birthday DATE NOT NULL
)
GO

CREATE TABLE class
(
	class_id INT PRIMARY KEY IDENTITY(1, 1),
	class_name NVARCHAR(25) NOT NULL,
	class_subject NVARCHAR(25) NOT NULL,
	teacher_id INT NOT NULL,
	FOREIGN KEY(teacher_id) REFERENCES teacher(teacher_id)
)
GO

CREATE TABLE student
(
	student_id INT PRIMARY KEY IDENTITY(1, 1),
	student_fullname NVARCHAR(255) NOT NULL,
	student_birthday DATE NOT NULL,
	student_address NVARCHAR(255) NOT NULL,
	student_class_id INT NOT NULL,
	FOREIGN KEY(student_class_id) REFERENCES class(class_id)
)

go

-- Insert data into teacher table
INSERT INTO teacher (teacher_fullname, teacher_birthday)
VALUES 
('Nguyen Van A', '1980-05-15'),
('Tran Thi B', '1975-08-22'),
('Le Van C', '1982-11-10'),
('Pham Thi D', '1985-02-05'),
('Hoang Van E', '1990-07-17');
GO

-- Insert data into class table
INSERT INTO class (class_name, class_subject, teacher_id)
VALUES 
('Math 101', 'Mathematics', 1),
('Eng 202', 'English', 2),
('Phys 303', 'Physics', 3),
('Chem 404', 'Chemistry', 4),
('Bio 505', 'Biology', 5);
GO

INSERT INTO student (student_fullname, student_birthday, student_address, student_class_id)
VALUES 
('Le Thi F', '2005-04-12', '123 Le Loi Street', 1),
('Nguyen Van G', '2004-09-23', '456 Tran Phu Street', 2),
('Tran Thi H', '2006-01-30', '789 Nguyen Trai Street', 3),
('Pham Van I', '2005-06-19', '101 Hoang Hoa Tham Street', 4),
('Hoang Thi J', '2004-12-05', '202 Nguyen Hue Street', 5),
('Nguyen Van K', '2005-07-21', '303 Le Duan Street', 1),
('Tran Thi L', '2004-11-02', '404 Tran Hung Dao Street', 2),
('Pham Van M', '2006-02-28', '505 Hai Ba Trung Street', 3),
('Hoang Thi N', '2005-09-15', '606 Ngo Quyen Street', 4),
('Le Van O', '2004-03-25', '707 Bui Thi Xuan Street', 5);
GO

INSERT INTO student (student_fullname, student_birthday, student_address, student_class_id)
VALUES 
('Nguyen Van X1', '2005-08-10', '808 Phan Chu Trinh Street', 1),
('Tran Thi Y2', '2006-03-15', '909 Le Thanh Ton Street', 2),
('Le Van Z3', '2004-07-20', '101 Nguyen Cong Tru Street', 3),
('Pham Thi W4', '2005-10-25', '202 Nguyen Van Cu Street', 4),
('Hoang Van Q5', '2006-04-05', '303 Le Van Sy Street', 5),
('Nguyen Thi P6', '2004-11-12', '404 Vo Thi Sau Street', 1),
('Tran Van S7', '2005-09-18', '505 Ton That Thuyet Street', 2),
('Le Thi R8', '2004-12-30', '606 Pham Ngu Lao Street', 3),
('Pham Van T9', '2006-01-28', '707 Dien Bien Phu Street', 4),
('Hoang Thi U10', '2005-05-08', '808 Le Hong Phong Street', 5),
('Nguyen Van X11', '2005-08-10', '808 Phan Chu Trinh Street', 1),
('Tran Thi Y12', '2006-03-15', '909 Le Thanh Ton Street', 2),
('Le Van Z13', '2004-07-20', '101 Nguyen Cong Tru Street', 3),
('Pham Thi W14', '2005-10-25', '202 Nguyen Van Cu Street', 4),
('Hoang Van Q15', '2006-04-05', '303 Le Van Sy Street', 5),
('Nguyen Thi P16', '2004-11-12', '404 Vo Thi Sau Street', 1),
('Tran Van S17', '2005-09-18', '505 Ton That Thuyet Street', 2),
('Le Thi R18', '2004-12-30', '606 Pham Ngu Lao Street', 3),
('Pham Van T19', '2006-01-28', '707 Dien Bien Phu Street', 4),
('Hoang Thi U20', '2005-05-08', '808 Le Hong Phong Street', 5),
('Nguyen Van X21', '2005-08-10', '808 Phan Chu Trinh Street', 1),
('Tran Thi Y22', '2006-03-15', '909 Le Thanh Ton Street', 2),
('Le Van Z23', '2004-07-20', '101 Nguyen Cong Tru Street', 3),
('Pham Thi W24', '2005-10-25', '202 Nguyen Van Cu Street', 4),
('Hoang Van Q25', '2006-04-05', '303 Le Van Sy Street', 5),
('Nguyen Thi P26', '2004-11-12', '404 Vo Thi Sau Street', 1),
('Tran Van S27', '2005-09-18', '505 Ton That Thuyet Street', 2),
('Le Thi R28', '2004-12-30', '606 Pham Ngu Lao Street', 3),
('Pham Van T29', '2006-01-28', '707 Dien Bien Phu Street', 4),
('Hoang Thi U30', '2005-05-08', '808 Le Hong Phong Street', 5),
('Nguyen Van X31', '2005-08-10', '808 Phan Chu Trinh Street', 1),
('Tran Thi Y32', '2006-03-15', '909 Le Thanh Ton Street', 2),
('Le Van Z33', '2004-07-20', '101 Nguyen Cong Tru Street', 3),
('Pham Thi W34', '2005-10-25', '202 Nguyen Van Cu Street', 4),
('Hoang Van Q35', '2006-04-05', '303 Le Van Sy Street', 5),
('Nguyen Thi P36', '2004-11-12', '404 Vo Thi Sau Street', 1),
('Tran Van S37', '2005-09-18', '505 Ton That Thuyet Street', 2),
('Le Thi R38', '2004-12-30', '606 Pham Ngu Lao Street', 3),
('Pham Van T39', '2006-01-28', '707 Dien Bien Phu Street', 4),
('Hoang Thi U40', '2005-05-08', '808 Le Hong Phong Street', 5),
('Nguyen Van X41', '2005-08-10', '808 Phan Chu Trinh Street', 1),
('Tran Thi Y42', '2006-03-15', '909 Le Thanh Ton Street', 2),
('Le Van Z43', '2004-07-20', '101 Nguyen Cong Tru Street', 3),
('Pham Thi W44', '2005-10-25', '202 Nguyen Van Cu Street', 4),
('Hoang Van Q45', '2006-04-05', '303 Le Van Sy Street', 5),
('Nguyen Thi P46', '2004-11-12', '404 Vo Thi Sau Street', 1),
('Tran Van S47', '2005-09-18', '505 Ton That Thuyet Street', 2),
('Le Thi R48', '2004-12-30', '606 Pham Ngu Lao Street', 3),
('Pham Van T49', '2006-01-28', '707 Dien Bien Phu Street', 4),
('Hoang Thi U50', '2005-05-08', '808 Le Hong Phong Street', 5);