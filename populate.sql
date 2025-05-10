-- ========================
-- 🌱 Sample Data Population
-- ========================

-- Insert Teachers
INSERT INTO "Teachers" ("NeptunCode", "Name", "Email", "Phone", "Address", "Password", "Age", "University", "Department")
VALUES 
('ABC01', 'Dr. John Smith', 'jsmith@uni.edu', '1234567890', '123 Main St', 'pass123', 45, 'Budapest University', 'Computer Science'),
('DEF02', 'Prof. Eva Nagy', 'enagy@uni.edu', '0987654321', '456 Elm St', 'pass456', 50, 'Debrecen Institute', 'Mathematics');

-- Insert Courses
INSERT INTO "Courses" ("Id", "Name", "Description", "Credits", "Semester", "TeacherNeptunCode")
VALUES
('11111111-1111-1111-1111-111111111111', 'Algorithms', 'Intro to algorithms and complexity', 5, 'Fall', 'ABC01'),
('22222222-2222-2222-2222-222222222222', 'Databases', 'SQL and relational theory', 4, 'Spring', 'ABC01'),
('33333333-3333-3333-3333-333333333333', 'Statistics', 'Basic statistics for data analysis', 3, 'Fall', 'DEF02');

-- Insert Students
INSERT INTO "Students" ("NeptunCode", "Name", "Email", "Phone", "Address", "Password", "Age", "State")
VALUES 
('STU01', 'Anna Kovács', 'anna@student.hu', '1111111111', 'Budapest 1', 'pw1', 20, 0),
('STU02', 'Béla Horváth', 'bela@student.hu', '2222222222', 'Pécs 2', 'pw2', 22, 3),
('STU03', 'Csilla Tóth', 'csilla@student.hu', '3333333333', 'Győr 3', 'pw3', 21, 1),
('STU04', 'Dávid Varga', 'david@student.hu', '4444444444', 'Debrecen 4', 'pw4', 23, 0);

-- Enroll students in courses (many-to-many)
INSERT INTO "CourseStudent" ("CoursesId", "StudentsNeptunCode") VALUES
('11111111-1111-1111-1111-111111111111', 'STU01'),
('11111111-1111-1111-1111-111111111111', 'STU02'),
('22222222-2222-2222-2222-222222222222', 'STU01'),
('22222222-2222-2222-2222-222222222222', 'STU03'),
('33333333-3333-3333-3333-333333333333', 'STU04'),
('33333333-3333-3333-3333-333333333333', 'STU02');