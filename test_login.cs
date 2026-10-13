using Microsoft.AspNetCore.Mvc;
using prog_part_2.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using static prog_part_2.Controllers.HashingController;

namespace Testing
{
    public class Test_login
    {

       
            private string testEmail = "test_login_user@email.com";
            private string testPass = "12345";

            [Fact]
            public void test_correctpassword()
            {
                hashing verify = new hashing();
                // make sure user exists first
                store_info store = new store_info();
                store.store_users_info("Test User", testEmail, testPass, "participant");

                var result = verify.login_user(testEmail, "12345") as OkObjectResult;

                Assert.NotNull(result);
                Assert.Equal("password verified", result.Value.ToString());
            }

            [Fact]
            public void test_wrong_password()
            {
                hashing verify = new hashing();
                var result = verify.login_user(testEmail, "wrong") as UnauthorizedObjectResult;

                // wrong password should return Unauthorized
                Assert.NotNull(result);
            }

            [Fact]
            public void test_organiser_can_manage_events()
            {
                store_info store = new store_info();
                // create organiser first - id 1 is organiser in your DB
                store.store_users_info("Organiser Test", "org@test.com", "12345", "organiser");
                string result_org = store.store_events_info("1", "Comrades", "Long run", "2026-06-15", "Durban", "run");
                Assert.Contains("successfully", result_org.ToLower());
            }

            [Fact]
            public void test_participant_rejected_for_organiser_function()
            {
                store_info store = new store_info();
                string result_part = store.store_events_info("2", "Test", "test", "2026-06-15", "JHB", "run");
                Assert.Equal("Access denied - organisers only can manage events", result_part);
            }

            [Fact]
            public void test_participant_can_enrol_and_recorded()
            {
                store_info store = new store_info();
                string result = store.store_enrolments_info("2", "1", "1", "2026-05-10", "Active");
                Assert.Contains("enrolment recorded", result.ToLower());
            }

            [Fact]
            public void test_enrolment_recorded_for_participant()
            {
                get_info get = new get_info();
                string result = get.get_enrolments_info("2");
                Assert.NotEqual("incorrect email or password", result);
            }

            [Fact]
            public void test_unauthenticated_login_fails()
            {
                get_info get = new get_info();
                string result = get.get_user_info("fake@email.com", "wrong");
                Assert.Equal("incorrect email or password", result);
            }
        }
    }









