﻿// OsmSharp - OpenStreetMap (OSM) SDK
// Copyright (C) 2013 Abelshausen Ben
// 
// This file is part of OsmSharp.
// 
// OsmSharp is free software: you can redistribute it and/or modify
// it under the terms of the GNU General Public License as published by
// the Free Software Foundation, either version 2 of the License, or
// (at your option) any later version.
// 
// OsmSharp is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the
// GNU General Public License for more details.
// 
// You should have received a copy of the GNU General Public License
// along with OsmSharp. If not, see <http://www.gnu.org/licenses/>.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using OsmSharp.Math.Geo;
using OsmSharp.Routing;
using OsmSharp.Routing.Instructions;
using OsmSharp.Routing.Osm.Interpreter;

namespace OsmSharp.Test.Unittests.Routing.Instructions
{
    /// <summary>
    /// Holds a number of unittests for generating routing instructions for tiny pieces of routes.
    /// </summary>
    [TestFixture]
    public class InstructionTests
    {
        /// <summary>
        /// Tests a simple turn.
        /// </summary>
        [Test]
        public void TestSimpleTurn()
        {
            Route route = new Route();
            route.Entries = new RoutePointEntry[3];
            route.Entries[0] = new RoutePointEntry()
            {
                Distance = 0,
                Latitude = 50.999f,
                Longitude = 4,
                Points = new RoutePoint[] { 
                    new RoutePoint() 
                    {
                        Latitude = 50.999f,
                        Longitude = 4,
                        Name = "Start"
                    }},
                SideStreets = null,
                Type = RoutePointEntryType.Start
            };
            route.Entries[1] = new RoutePointEntry()
            {
                Distance = 0,
                Latitude = 51,
                Longitude = 4,
                Tags = new RouteTags[] {
                    new RouteTags() { Key = "name", Value = "Street A" },
                    new RouteTags() { Key = "highway", Value = "residential" }
                },
                Type = RoutePointEntryType.Along,
                SideStreets = new RoutePointEntrySideStreet[] {
                    new RoutePointEntrySideStreet() { 
                        Latitude = 51, 
                        Longitude = 3.999f,
                        Tags = new RouteTags[] {
                            new RouteTags() { Key = "name", Value = "Street B" },
                            new RouteTags() { Key = "highway", Value = "residential" }
                        },
                        WayName = "Street B"
                    }
                }
            };
            route.Entries[2] = new RoutePointEntry()
            {
                Distance = 0,
                Latitude = 51,
                Longitude = 4.001f,
                Tags = new RouteTags[] {
                    new RouteTags() { Key = "name", Value = "Street B" },
                    new RouteTags() { Key = "highway", Value = "residential" }
                },
                Type = RoutePointEntryType.Stop,
                Points = new RoutePoint[] { 
                    new RoutePoint() 
                    {
                        Latitude = 51,
                        Longitude = 4.001f,
                        Name = "Stop"
                    }},
            };

            // create the language generator.
            var languageGenerator = new LanguageTestGenerator();

            // generate instructions.
            List<Instruction> instructions = InstructionGenerator.Generate(route, new OsmRoutingInterpreter(), languageGenerator);
            Assert.AreEqual(3, instructions.Count);
            Assert.AreEqual("GenerateDirectTurn:0_Right_0", instructions[1].Text);
        }

//        /// <summary>
//        /// Tests a simple straight-on instruction.
//        /// </summary>
//        [Test]
//        public void TestStraightOn() 
//        {
//            GeoCoordinate left = new GeoCoordinate(51, 3.999);
//            GeoCoordinate right = new GeoCoordinate(51, 4.001);
//            GeoCoordinate top = new GeoCoordinate(51.001, 4);
//            GeoCoordinate bottom = new GeoCoordinate(50.999, 4);
//            GeoCoordinate center = new GeoCoordinate(51, 4);
//
//            
//            Route route = new Route();
//            route.Entries = new RoutePointEntry[3];
//            route.Entries[0] = new RoutePointEntry()
//            {
//                Distance = 0,
//                Latitude = (float)bottom.Latitude,
//                Longitude = (float)bottom.Longitude,
//                Points = new RoutePoint[] { 
//                    new RoutePoint() 
//                    {
//                        Latitude = (float)bottom.Latitude,
//                        Longitude = (float)bottom.Longitude,
//                        Name = "Start"
//                    }},
//                SideStreets = null,
//                Type = RoutePointEntryType.Start
//            };
//            route.Entries[1] = new RoutePointEntry()
//            {
//                Distance = 0,
//                Latitude = (float)center.Latitude,
//                Longitude = (float)center.Longitude,
//                Type = RoutePointEntryType.Along,
//                SideStreets = new RoutePointEntrySideStreet[] {
//                    new RoutePointEntrySideStreet() { 
//                        Latitude = (float)left.Latitude,
//                        Longitude = (float)left.Longitude,
//                        Tags = new RouteTags[] {
//                            new RouteTags() { Key = "name", Value = "Street B" },
//                            new RouteTags() { Key = "highway", Value = "residential" }
//                        },
//                        WayName = "Street B"
//                    },
//                    new RoutePointEntrySideStreet() { 
//                        Latitude = (float)right.Latitude,
//                        Longitude = (float)right.Longitude,
//                        Tags = new RouteTags[] {
//                            new RouteTags() { Key = "name", Value = "Street B" },
//                            new RouteTags() { Key = "highway", Value = "residential" }
//                        },
//                        WayName = "Street B"
//                    }
//                }
//            };
//            route.Entries[2] = new RoutePointEntry()
//            {
//                Distance = 0,
//                Latitude = (float)top.Latitude,
//                Longitude = (float)top.Longitude,
//                Tags = new RouteTags[] {
//                    new RouteTags() { Key = "name", Value = "Street A" },
//                    new RouteTags() { Key = "highway", Value = "residential" }
//                },
//                Type = RoutePointEntryType.Stop,
//                Points = new RoutePoint[] { 
//                    new RoutePoint() 
//                    {
//                        Latitude = (float)top.Latitude,
//                        Longitude = (float)top.Longitude,
//                        Name = "Stop"
//                    }}
//            };
//
//            // create the language generator.
//            var languageGenerator = new LanguageTestGenerator();
//
//            // generate instructions.
//            List<Instruction> instructions = InstructionGenerator.Generate(route, new OsmRoutingInterpreter(), languageGenerator);
//        }
//
//        /// <summary>
//        /// Tests a simple roundabout instruction.
//        /// </summary>
//        [Test]
//        public void TestRoundabout() 
//        {
//            GeoCoordinate westWest = new GeoCoordinate(51, 3.998);
//            GeoCoordinate west = new GeoCoordinate(51, 3.999);
//            GeoCoordinate eastEast = new GeoCoordinate(51, 4.002);
//            GeoCoordinate east = new GeoCoordinate(51, 4.001);
//            GeoCoordinate north = new GeoCoordinate(51.001, 4);
//            GeoCoordinate northNorth = new GeoCoordinate(51.002, 4);
//            GeoCoordinate south = new GeoCoordinate(50.999, 4);
//            GeoCoordinate southSouth = new GeoCoordinate(50.998, 4);
//            GeoCoordinate center = new GeoCoordinate(51, 4);
//
//            Route route = new Route();
//            route.Entries = new RoutePointEntry[3];
//            route.Entries[0] = new RoutePointEntry()
//            {
//                Distance = 0,
//                Latitude = (float)southSouth.Latitude,
//                Longitude = (float)southSouth.Longitude,
//                Points = new RoutePoint[] { 
//                    new RoutePoint() 
//                    {
//                        Latitude = (float)southSouth.Latitude,
//                        Longitude = (float)southSouth.Longitude,
//                        Name = "Start"
//                    }},
//                SideStreets = null,
//                Type = RoutePointEntryType.Start
//            };
//            route.Entries[1] = new RoutePointEntry()
//            {
//                Distance = 0,
//                Latitude = (float)south.Latitude,
//                Longitude = (float)south.Longitude,
//                SideStreets = null,
//                Type = RoutePointEntryType.Along,
//                WayFromName = "SouthStreet",
//                Tags = new RouteTags[] {
//                    new RouteTags() { Key = "name", Value = "SouthStreet" },
//                    new RouteTags() { Key = "highway", Value = "residential" }
//                },
//                SideStreets = new RoutePointEntrySideStreet[] {
//                    new RoutePointEntrySideStreet() { 
//                        Latitude = (float)left.Latitude,
//                        Longitude = (float)left.Longitude,
//                        Tags = new RouteTags[] {
//                            new RouteTags() { Key = "junction", Value = "roundabout" },
//                            new RouteTags() { Key = "highway", Value = "residential" }
//                        },
//                        WayName = "Street B"
//                    },
//                    new RoutePointEntrySideStreet() { 
//                        Latitude = (float)right.Latitude,
//                        Longitude = (float)right.Longitude,
//                        Tags = new RouteTags[] {
//                            new RouteTags() { Key = "junction", Value = "roundabout" },
//                            new RouteTags() { Key = "highway", Value = "residential" }
//                        },
//                        WayName = "Street B"
//                    }
//                }
//            };
//            route.Entries[2] = new RoutePointEntry()
//            {
//                Distance = 0,
//                Latitude = (float)east.Latitude,
//                Longitude = (float)east.Longitude,
//                SideStreets = null,
//                Type = RoutePointEntryType.Along,
//                Tags = new RouteTags[] {
//                    new RouteTags() { Key = "junction", Value = "roundabout" },
//                    new RouteTags() { Key = "highway", Value = "residential" }
//                },
//                SideStreets = new RoutePointEntrySideStreet[] {
//                    new RoutePointEntrySideStreet() { 
//                        Latitude = (float)eastEast.Latitude,
//                        Longitude = (float)eastEast.Longitude,
//                        Tags = new RouteTags[] {
//                            new RouteTags() { Key = "name", Value = "EastStreet" },
//                            new RouteTags() { Key = "highway", Value = "residential" }
//                        },
//                        WayName = "EastStreet"
//                    }
//                }
//            };
//            route.Entries[3] = new RoutePointEntry()
//            {
//                Distance = 0,
//                Latitude = (float)north.Latitude,
//                Longitude = (float)north.Longitude,
//                SideStreets = null,
//                Type = RoutePointEntryType.Along,
//                Tags = new RouteTags[] {
//                    new RouteTags() { Key = "junction", Value = "roundabout" },
//                    new RouteTags() { Key = "highway", Value = "residential" }
//                },
//                SideStreets = new RoutePointEntrySideStreet[] {
//                    new RoutePointEntrySideStreet() { 
//                        Latitude = (float)west.Latitude,
//                        Longitude = (float)west.Longitude,
//                        Tags = new RouteTags[] {
//                            new RouteTags() { Key = "junction", Value = "roundabout" },
//                            new RouteTags() { Key = "highway", Value = "residential" }
//                        }
//                    }
//                }
//            };
//            route.Entries[4] = new RoutePointEntry()
//            {
//                Distance = 0,
//                Latitude = (float)northNorth.Latitude,
//                Longitude = (float)northNorth.Longitude,
//                SideStreets = null,
//                Type = RoutePointEntryType.Stop,
//                Tags = new RouteTags[] {
//                    new RouteTags() { Key = "name", Value = "NorthStreet" },
//                    new RouteTags() { Key = "highway", Value = "residential" }
//                },
//                Points = new RoutePoint[] { 
//                    new RoutePoint() 
//                    {
//                        Latitude = (float)top.Latitude,
//                        Longitude = (float)top.Longitude,
//                        Name = "Stop"
//                    }}
//            };
//
//            // create the language generator.
//            var languageGenerator = new LanguageTestGenerator();
//
//            // generate instructions.
//            List<Instruction> instructions = InstructionGenerator.Generate(route, new OsmRoutingInterpreter(), languageGenerator);
//
//        }
    }
}