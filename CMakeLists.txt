cmake_minimum_required(VERSION 3.24)
project(OpenGLProject)

# 设置 C++ 标准
set(CMAKE_CXX_STANDARD 17)
set(CMAKE_CXX_STANDARD_REQUIRED ON)

# 设置编译器
set(CMAKE_C_COMPILER gcc)
set(CMAKE_CXX_COMPILER g++)

# 包含目录
include_directories(include)

# 库目录
link_directories(lib)

add_library(glad src/glad.c)
add_library(stb_image src/image.cpp)

# 添加可执行文件
# add_executable(${PROJECT_NAME} src/box.cpp)
# add_executable(${PROJECT_NAME} src/light.cpp)
# add_executable(${PROJECT_NAME} src/multi_lights.cpp)
add_executable(${PROJECT_NAME} src/load_mesh.cpp)

# 链接库
target_link_libraries(${PROJECT_NAME}
    glfw3
    glad
    opengl32
    stb_image
    assimp
    zlib
)