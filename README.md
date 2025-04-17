# 📘 BookingApp

![C#](https://img.shields.io/badge/C%23-8.0-blue?logo=c-sharp)
![.NET](https://img.shields.io/badge/.NET-Framework%204.7.2-blueviolet?logo=dotnet)
![WPF](https://img.shields.io/badge/WPF-Windows%20Presentation%20Foundation-lightblue)
![MVVM](https://img.shields.io/badge/Architecture-MVVM-green)
![CSV](https://img.shields.io/badge/Data%20Storage-CSV-yellow)
![ConsoleApp](https://img.shields.io/badge/Console%20App-.NET%20Console-bluegrey)

**BookingApp** is a feature-rich **desktop** and **console-based** application built with **C#** and **WPF**, designed to streamline the management of **accommodations**, **tours**, **reservations**, and **user interactions**. 

The application is structured using the **MVVM (Model-View-ViewModel)** design pattern for WPF and clean architectural principles for the console version. It supports multiple user roles — **Owners**, **Guests**, **Guides**, and **Tourists** — each with tailored functionalities.

> 🟢 This README emphasizes the **Guest role**, which was actively developed and tested by me.

## ✨ Features

### 🔄 Multi-Role Functionality

| Role        | Key Functionalities |
|-------------|---------------------|
| **Guest**   | Book accommodations, manage reservations, rate hosts, use forums |
| **Owner**   | Manage accommodations, view stats, respond to reviews, handle booking changes |
| **Guide**   | Create/cancel tours, track tour activity, review statistics |
| **Tourist** | Book tours, rate experiences, request complex tours, win vouchers |

Each role includes standard and **"Super-user"** privileges, unlocking advanced features like forum interaction, renovations scheduling, tour management, and more.

---

## 🎯 Guest Experience (My Role)

As a **Guest**, you interact primarily with accommodations, bookings, and community features. This role is designed to provide a smooth and intuitive reservation experience.

### 🧳 Guest Functionalities

| Capability                              | Description                                                                 |
|-----------------------------------------|-----------------------------------------------------------------------------|
| 🔍 **Search Accommodations**           | Browse listings filtered by location, availability, and preferences         |
| 🏨 **Book Accommodations**             | Instantly reserve available stays                                           |
| 🌟 **Rate Accommodations & Hosts**     | Leave feedback and ratings post-visit                                      |
| 🛠 **Propose Renovations**             | Suggest improvements for better guest experience                           |
| 📝 **View Guest Reviews**              | Read shared experiences from other users                                   |
| 🔄 **Reschedule Reservations**         | Send requests to change reservation dates                                  |
| ❌ **Cancel Reservations**             | Cancel planned stays directly from the console                             |
| 🌍 **Anywhere / Anytime Booking**      | System-recommended stays based on flexible criteria                        |
| 🏅 **Super-Guest Features**            | Access and contribute to the forum, leave and reply to comments            |

> 🏆 Super-Guest status unlocks community engagement via the built-in forum system.

---

## 🔐 Login Credentials

Use the following credentials to log in with different roles:

| Role        | Username   | Password   |
|-------------|------------|------------|
| **Guest**   | `gost`     | `gost`     |
| Owner       | `vlasnik`  | `vlasnik`  |
| Guide       | `vodic`    | `vodic`    |
| Tourist     | `turista`  | `turista`  |

> 🔐 These are default development credentials.

---

## 🛠️ Technologies Used

| Category         | Technology                                  |
|------------------|---------------------------------------------|
| Language         | C#                                          |
| UI Framework     | WPF (Windows Presentation Foundation)       |
| Architecture     | MVVM (Model-View-ViewModel)                 |
| Console I/O      | .NET Console Application                    |
| Data Storage     | CSV Files                                   |
| Serialization    | Custom CSV Serializers                      |

---

## 📁 Project Structure

```bash
BookingApp/
├── Model/
│   ├── Accommodation.cs
│   ├── Reservation.cs
│   ├── Tour.cs
│   └── ...
├── ViewModel/
│   ├── Owner/
│   ├── Tourist/
│   └── ...
├── View/
│   ├── CommentForm.xaml
│   ├── CommentsOverview.xaml
│   └── ...
├── Repository/
│   ├── AccommodationRepository.cs
│   └── ...
├── Service/
│   ├── AccommodationService.cs
│   └── ...
├── Dto/
│   ├── AccommodationDto.cs
│   └── ...
├── Resources/
│   ├── Data/
│   │   ├── accommodations.csv
│   │   └── ...
│   ├── Images/
├── Serializer/
│   └── Serializer.cs
```

> 🧠 The structure reflects a clear separation of concerns, adhering to MVVM and SOLID principles.

---

## 🚀 Installation

### ✅ Prerequisites

- OS: **Windows**
- .NET Framework **4.7.2** or later
- IDE: **Visual Studio 2019+**

### 📥 Installation Steps

1. **Clone the repository**
   ```bash
   git clone https://github.com/MarkoKolarski/Booking_app.git
   ```

2. **Open the solution** in Visual Studio.

3. **Restore NuGet packages** (if required).

4. **Build the project**
   ```bash
   Ctrl + Shift + B
   ```

5. **Run the app**
   - Use Visual Studio's `Start Debugging` (`F5`)
   - Or run the output `.exe` from `/bin/Debug`

---

## 📌 Usage

### ▶️ Guest Role Walkthrough

1. **Login using:**
   ```
   Username: gost
   Password: gost
   ```

2. **Access the main Guest Menu**, where you can:
   - Search and book accommodations
   - View or cancel current reservations
   - Submit reviews or reschedule
   - Participate in forum discussions (if Super-Guest)

3. **Navigate via numbered options** in the console or UI-based forms in the WPF version.
