﻿using DogRallyAPI.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;

namespace DogRallyAPI.Models
{
    public class SeedData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new DogRallyContext(
                serviceProvider.GetRequiredService<
                    DbContextOptions<DogRallyContext>>()))
            {
                context.Database.EnsureCreated();

                var userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();
                var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();

                // Check if the DB has been seeded
                if (context.Exercises.Any() && context.Users.Any())
                {
                    return;   // DB has been seeded
                }

                // Add roles and users
                string[] roles = new string[] { "Admin", "User" };

                foreach (var role in roles)
                {
                    if (!roleManager.RoleExistsAsync(role).Result)
                    {
                        roleManager.CreateAsync(new IdentityRole(role)).Wait();
                    }
                }

                // Seed admin user
                var adminUser = new ApplicationUser
                {
                    UserName = "admin@dogrally.com",
                    Email = "admin@dogrally.com",
                    EmailConfirmed = true
                };


                if (userManager.FindByEmailAsync(adminUser.Email).Result == null)
                {
                    var result = userManager.CreateAsync(adminUser, "Admin123!").Result;
                    if (result.Succeeded)
                    {
                        userManager.AddToRoleAsync(adminUser, "Admin").Wait();
                    }
                }

                // Seed regular user
                var regularUser = new ApplicationUser
                {
                    UserName = "regular@dogrally.com",
                    Email = "regular@dogrally.com",
                    EmailConfirmed = true
                };

                if (userManager.FindByEmailAsync(regularUser.Email).Result == null)
                {
                    var result = userManager.CreateAsync(regularUser, "Regular123!").Result;
                    if (result.Succeeded)
                    {
                        userManager.AddToRoleAsync(regularUser, "User").Wait();
                    }
                }



                // Seed Exercises data
                if (!context.Exercises.Any())
                {
                    context.Exercises.AddRange(

                        // Beginner
                        new Exercise
                        {
                            ExerciseName = "Højresving",
                            ExerciseMovementEnumID = PaceEnum.Walk,
                            ExerciseSideShift = false,
                            ExerciseIllustrationPath = "/images/exercises/3.png",
                            ExerciseClassEnumID = ClassEnum.Beginner,
                            ExerciseSignNumber = 3,
                            ExercisePositionX = 10,
                            ExercisePositionY = 75
                        },

                        new Exercise
                        {
                            ExerciseName = "Venstresving",
                            ExerciseMovementEnumID = PaceEnum.Walk,
                            ExerciseSideShift = false,
                            ExerciseIllustrationPath = "/images/exercises/4.png",
                            ExerciseClassEnumID = ClassEnum.Beginner,
                            ExerciseSignNumber = 4,
                            ExercisePositionX = 10,
                            ExercisePositionY = 150
                        },

                        new Exercise
                        {
                            ExerciseName = "270° Højre Rundt",
                            ExerciseMovementEnumID = PaceEnum.Walk,
                            ExerciseSideShift = false,
                            ExerciseIllustrationPath = "/images/exercises/5.png",
                            ExerciseClassEnumID = ClassEnum.Beginner,
                            ExerciseSignNumber = 5,
                            ExercisePositionX = 10,
                            ExercisePositionY = 225
                        },

                        new Exercise
                        {
                            ExerciseName = "270° Venstre Rundt",
                            ExerciseMovementEnumID = PaceEnum.Walk,
                            ExerciseSideShift = false,
                            ExerciseIllustrationPath = "/images/exercises/6.png",
                            ExerciseClassEnumID = ClassEnum.Beginner,
                            ExerciseSignNumber = 6,
                            ExercisePositionX = 10,
                            ExercisePositionY = 300
                        },

                        new Exercise
                        {
                            ExerciseName = "Diagonalt Højre",
                            ExerciseMovementEnumID = PaceEnum.Walk,
                            ExerciseSideShift = false,
                            ExerciseIllustrationPath = "/images/exercises/7.png",
                            ExerciseClassEnumID = ClassEnum.Beginner,
                            ExerciseSignNumber = 7,
                            ExercisePositionX = 10,
                            ExercisePositionY = 375
                        },

                        // Trained
                        new Exercise
                        {
                            ExerciseName = "Slalom med rundtur",
                            ExerciseMovementEnumID = PaceEnum.Walk,
                            ExerciseSideShift = false,
                            ExerciseIllustrationPath = "/images/exercises/100.png",
                            ExerciseClassEnumID = ClassEnum.Trained,
                            ExerciseSignNumber = 100,
                            ExercisePositionX = 40,
                            ExercisePositionY = 75
                        },

                        new Exercise
                        {
                            ExerciseName = "Fristende 8-tal",
                            ExerciseMovementEnumID = PaceEnum.Walk,
                            ExerciseSideShift = false,
                            ExerciseIllustrationPath = "/images/exercises/101.png",
                            ExerciseClassEnumID = ClassEnum.Trained,
                            ExerciseSignNumber = 101,
                            ExercisePositionX = 40,
                            ExercisePositionY = 150
                        },

                        new Exercise
                        {
                            ExerciseName = "Kløverbladet",
                            ExerciseMovementEnumID = PaceEnum.Walk,
                            ExerciseSideShift = false,
                            ExerciseIllustrationPath = "/images/exercises/102.png",
                            ExerciseClassEnumID = ClassEnum.Trained,
                            ExerciseSignNumber = 102,
                            ExercisePositionX = 40,
                            ExercisePositionY = 225
                        },

                        new Exercise
                        {
                            ExerciseName = "Send over spring",
                            ExerciseMovementEnumID = PaceEnum.Walk,
                            ExerciseSideShift = false,
                            ExerciseIllustrationPath = "/images/exercises/103.png",
                            ExerciseClassEnumID = ClassEnum.Trained,
                            ExerciseSignNumber = 103,
                            ExercisePositionX = 40,
                            ExercisePositionY = 300
                        },

                        new Exercise
                        {
                            ExerciseName = "Stop - bliv - alm gang forbi 1 spring",
                            ExerciseMovementEnumID = PaceEnum.Walk,
                            ExerciseSideShift = false,
                            ExerciseIllustrationPath = "/images/exercises/104.png",
                            ExerciseClassEnumID = ClassEnum.Trained,
                            ExerciseSignNumber = 104,
                            ExercisePositionX = 40,
                            ExercisePositionY = 375
                        },

                        // Expert
                        new Exercise
                        {
                            ExerciseName = "Spring - ro - spring",
                            ExerciseMovementEnumID = PaceEnum.Walk,
                            ExerciseSideShift = false,
                            ExerciseIllustrationPath = "/images/exercises/200.png",
                            ExerciseClassEnumID = ClassEnum.Expert,
                            ExerciseSignNumber = 200,
                            ExercisePositionX = 70,
                            ExercisePositionY = 75
                        },

                        new Exercise
                        {
                            ExerciseName = "Dæk - bliv - alm gang forbi 1 spring",
                            ExerciseMovementEnumID = PaceEnum.Walk,
                            ExerciseSideShift = false,
                            ExerciseIllustrationPath = "/images/exercises/201.png",
                            ExerciseClassEnumID = ClassEnum.Expert,
                            ExerciseSignNumber = 201,
                            ExercisePositionX = 70,
                            ExercisePositionY = 150
                        },

                        new Exercise
                        {
                            ExerciseName = "Dæk - bliv - løb forbi 1 spring",
                            ExerciseMovementEnumID = PaceEnum.Walk,
                            ExerciseSideShift = false,
                            ExerciseIllustrationPath = "/images/exercises/202.png",
                            ExerciseClassEnumID = ClassEnum.Expert,
                            ExerciseSignNumber = 202,
                            ExercisePositionX = 70,
                            ExercisePositionY = 225
                        },

                        new Exercise
                        {
                            ExerciseName = "Stå - bliv - alm gang forbi 1 spring",
                            ExerciseMovementEnumID = PaceEnum.Walk,
                            ExerciseSideShift = false,
                            ExerciseIllustrationPath = "/images/exercises/203.png",
                            ExerciseClassEnumID = ClassEnum.Expert,
                            ExerciseSignNumber = 203,
                            ExercisePositionX = 70,
                            ExercisePositionY = 300
                        },

                        new Exercise
                        {
                            ExerciseName = "Stå - bliv - løb forbi 1 spring",
                            ExerciseMovementEnumID = PaceEnum.Walk,
                            ExerciseSideShift = false,
                            ExerciseIllustrationPath = "/images/exercises/204.png",
                            ExerciseClassEnumID = ClassEnum.Expert,
                            ExerciseSignNumber = 204,
                            ExercisePositionX = 70,
                            ExercisePositionY = 375
                        },

                        // Champion
                        new Exercise
                        {
                            ExerciseName = "Sideskift foran",
                            ExerciseMovementEnumID = PaceEnum.Walk,
                            ExerciseSideShift = false,
                            ExerciseIllustrationPath = "/images/exercises/300.png",
                            ExerciseClassEnumID = ClassEnum.Champion,
                            ExerciseSignNumber = 300,
                            ExercisePositionX = 100,
                            ExercisePositionY = 75
                        },

                        new Exercise
                        {
                            ExerciseName = "90° venstre sideskift om kegle",
                            ExerciseMovementEnumID = PaceEnum.Walk,
                            ExerciseSideShift = false,
                            ExerciseIllustrationPath = "/images/exercises/301.png",
                            ExerciseClassEnumID = ClassEnum.Trained,
                            ExerciseSignNumber = 301,
                            ExercisePositionX = 100,
                            ExercisePositionY = 150
                        },

                        new Exercise
                        {
                            ExerciseName = "90° højre sideskift om kegle",
                            ExerciseMovementEnumID = PaceEnum.Walk,
                            ExerciseSideShift = false,
                            ExerciseIllustrationPath = "/images/exercises/302.png",
                            ExerciseClassEnumID = ClassEnum.Trained,
                            ExerciseSignNumber = 302,
                            ExercisePositionX = 100,
                            ExercisePositionY = 225
                        },

                        new Exercise
                        {
                            ExerciseName = "Tunnel",
                            ExerciseMovementEnumID = PaceEnum.Walk,
                            ExerciseSideShift = false,
                            ExerciseIllustrationPath = "/images/exercises/303.png",
                            ExerciseClassEnumID = ClassEnum.Trained,
                            ExerciseSignNumber = 303,
                            ExercisePositionX = 100,
                            ExercisePositionY = 300
                        },

                        new Exercise
                        {
                            ExerciseName = "1 skridt - 2 skridt - hund bakker",
                            ExerciseMovementEnumID = PaceEnum.Walk,
                            ExerciseSideShift = false,
                            ExerciseIllustrationPath = "/images/exercises/304.png",
                            ExerciseClassEnumID = ClassEnum.Trained,
                            ExerciseSignNumber = 304,
                            ExercisePositionX = 100,
                            ExercisePositionY = 375
                        }
                    );
                    context.SaveChanges();
                }
            }
        }
    }
}
