﻿using System;
using System.IO;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using SatisfactorySaveParser.Save;
using SatisfactorySaveParser.Save.Serialization;

namespace SatisfactorySaveParser.Tests.Save
{
    [TestClass]
    public class SaveHeaderTests
    {
        private static readonly byte[] SaveHeaderV5Bytes = new byte[] { 0x05, 0x00, 0x00, 0x00, 0x11, 0x00, 0x00, 0x00, 0xF9, 0x02, 0x01, 0x00, 0x11, 0x00, 0x00, 0x00, 0x50, 0x65, 0x72, 0x73, 0x69, 0x73, 0x74, 0x65, 0x6E, 0x74, 0x5F, 0x4C, 0x65, 0x76, 0x65, 0x6C, 0x00, 0x47, 0x00, 0x00, 0x00, 0x3F, 0x73, 0x74, 0x61, 0x72, 0x74, 0x6C, 0x6F, 0x63, 0x3D, 0x47, 0x72, 0x61, 0x73, 0x73, 0x20, 0x46, 0x69, 0x65, 0x6C, 0x64, 0x73, 0x3F, 0x73, 0x65, 0x73, 0x73, 0x69, 0x6F, 0x6E, 0x4E, 0x61, 0x6D, 0x65, 0x3D, 0x73, 0x70, 0x61, 0x63, 0x65, 0x20, 0x77, 0x61, 0x72, 0x3F, 0x56, 0x69, 0x73, 0x69, 0x62, 0x69, 0x6C, 0x69, 0x74, 0x79, 0x3D, 0x53, 0x56, 0x5F, 0x46, 0x72, 0x69, 0x65, 0x6E, 0x64, 0x73, 0x4F, 0x6E, 0x6C, 0x79, 0x00, 0x0A, 0x00, 0x00, 0x00, 0x73, 0x70, 0x61, 0x63, 0x65, 0x20, 0x77, 0x61, 0x72, 0x00, 0xC5, 0xAB, 0x00, 0x00, 0xD0, 0xDA, 0x51, 0x19, 0x8E, 0xA4, 0xD6, 0x08, 0x01 };
        private const FSaveHeaderVersion SaveHeaderV5HeaderVersion = FSaveHeaderVersion.AddedSessionVisibility;
        private const FSaveCustomVersion SaveHeaderV5SaveVersion = FSaveCustomVersion.RenamedSaveSessionId;
        private const int SaveHeaderV5BuildVersion = 66297;
        private const string SaveHeaderV5MapName = "Persistent_Level";
        private const string SaveHeaderV5MapOptions = "?startloc=Grass Fields?sessionName=space war?Visibility=SV_FriendsOnly";
        private const string SaveHeaderV5SessionName = "space war";
        private const int SaveHeaderV5PlayDuration = 0x0000ABC5;
        private const long SaveHeaderV5SaveDateTime = 0x08D6A48E1951DAD0;
        private const ESessionVisibility SaveHeaderV5SessionVisibility = ESessionVisibility.SV_FriendsOnly;

        private static readonly byte[] SaveHeaderV4Bytes = new byte[] { 0x04, 0x00, 0x00, 0x00, 0x10, 0x00, 0x00, 0x00, 0xF9, 0x02, 0x01, 0x00, 0x11, 0x00, 0x00, 0x00, 0x50, 0x65, 0x72, 0x73, 0x69, 0x73, 0x74, 0x65, 0x6E, 0x74, 0x5F, 0x4C, 0x65, 0x76, 0x65, 0x6C, 0x00, 0x28, 0x00, 0x00, 0x00, 0x3F, 0x73, 0x74, 0x61, 0x72, 0x74, 0x6C, 0x6F, 0x63, 0x3D, 0x47, 0x72, 0x61, 0x73, 0x73, 0x20, 0x46, 0x69, 0x65, 0x6C, 0x64, 0x73, 0x3F, 0x73, 0x65, 0x73, 0x73, 0x69, 0x6F, 0x6E, 0x4E, 0x61, 0x6D, 0x65, 0x3D, 0x54, 0x65, 0x73, 0x74, 0x00, 0x05, 0x00, 0x00, 0x00, 0x54, 0x65, 0x73, 0x74, 0x00, 0x8C, 0xBE, 0x00, 0x00, 0x60, 0x5D, 0xC7, 0xFF, 0x4D, 0x71, 0xD6, 0x08 };
        private const FSaveHeaderVersion SaveHeaderV4HeaderVersion = FSaveHeaderVersion.SessionIDStringAndSaveTimeAdded;
        private const FSaveCustomVersion SaveHeaderV4SaveVersion = FSaveCustomVersion.WireSpanFromConnnectionComponents;
        private const int SaveHeaderV4BuildVersion = 66297;
        private const string SaveHeaderV4MapName = "Persistent_Level";
        private const string SaveHeaderV4MapOptions = "?startloc=Grass Fields?sessionName=Test";
        private const string SaveHeaderV4SessionName = "Test";
        private const int SaveHeaderV4PlayDuration = 0x0000BE8C;
        private const long SaveHeaderV4SaveDateTime = 0x08D6714DFFC75D60;

        [TestMethod]
        public void SaveHeaderV5Reading()
        {
            using (var stream = new MemoryStream(SaveHeaderV5Bytes))
            using (var reader = new BinaryReader(stream))
            {
                var header = SatisfactorySaveSerializer.DeserializeHeader(reader);

                Assert.AreEqual(SaveHeaderV5HeaderVersion, header.HeaderVersion);
                Assert.AreEqual(SaveHeaderV5SaveVersion, header.SaveVersion);
                Assert.AreEqual(SaveHeaderV5BuildVersion, header.BuildVersion);

                Assert.AreEqual(SaveHeaderV5MapName, header.MapName);
                Assert.AreEqual(SaveHeaderV5MapOptions, header.MapOptions);
                Assert.AreEqual(SaveHeaderV5SessionName, header.SessionName);

                Assert.AreEqual(SaveHeaderV5PlayDuration, header.PlayDuration);
                Assert.AreEqual(SaveHeaderV5SaveDateTime, header.SaveDateTime);
                Assert.AreEqual(SaveHeaderV5SessionVisibility, header.SessionVisibility);

                Assert.AreEqual(stream.Length, stream.Position);
            }
        }

        [TestMethod]
        public void SaveHeaderV5Writing()
        {
            using (var stream = new MemoryStream())
            using (var writer = new BinaryWriter(stream))
            {
                var header = new FSaveHeader
                {
                    HeaderVersion = SaveHeaderV5HeaderVersion,
                    SaveVersion = SaveHeaderV5SaveVersion,
                    BuildVersion = SaveHeaderV5BuildVersion,

                    MapName = SaveHeaderV5MapName,
                    MapOptions = SaveHeaderV5MapOptions,
                    SessionName = SaveHeaderV5SessionName,

                    PlayDuration = SaveHeaderV5PlayDuration,
                    SaveDateTime = SaveHeaderV5SaveDateTime,
                    SessionVisibility = SaveHeaderV5SessionVisibility
                };

                SatisfactorySaveSerializer.SerializeHeader(header, writer);

                CollectionAssert.AreEqual(SaveHeaderV5Bytes, stream.ToArray());
            }
        }

        [TestMethod]
        public void SaveHeaderV4Reading()
        {
            using (var stream = new MemoryStream(SaveHeaderV4Bytes))
            using (var reader = new BinaryReader(stream))
            {
                var header = SatisfactorySaveSerializer.DeserializeHeader(reader);

                Assert.AreEqual(SaveHeaderV4HeaderVersion, header.HeaderVersion);
                Assert.AreEqual(SaveHeaderV4SaveVersion, header.SaveVersion);
                Assert.AreEqual(SaveHeaderV4BuildVersion, header.BuildVersion);

                Assert.AreEqual(SaveHeaderV4MapName, header.MapName);
                Assert.AreEqual(SaveHeaderV4MapOptions, header.MapOptions);
                Assert.AreEqual(SaveHeaderV4SessionName, header.SessionName);

                Assert.AreEqual(SaveHeaderV4PlayDuration, header.PlayDuration);
                Assert.AreEqual(SaveHeaderV4SaveDateTime, header.SaveDateTime);

                Assert.AreEqual(stream.Length, stream.Position);
            }
        }

        [TestMethod]
        public void SaveHeaderV4Writing()
        {
            using (var stream = new MemoryStream())
            using (var writer = new BinaryWriter(stream))
            {
                var header = new FSaveHeader
                {
                    HeaderVersion = SaveHeaderV4HeaderVersion,
                    SaveVersion = SaveHeaderV4SaveVersion,
                    BuildVersion = SaveHeaderV4BuildVersion,

                    MapName = SaveHeaderV4MapName,
                    MapOptions = SaveHeaderV4MapOptions,
                    SessionName = SaveHeaderV4SessionName,

                    PlayDuration = SaveHeaderV4PlayDuration,
                    SaveDateTime = SaveHeaderV4SaveDateTime
                };

                SatisfactorySaveSerializer.SerializeHeader(header, writer);

                CollectionAssert.AreEqual(SaveHeaderV4Bytes, stream.ToArray());
            }
        }

        [TestMethod]
        public void SessionVisibilityCompatibility()
        {
            using (var stream = new MemoryStream(SaveHeaderV5Bytes))
            using (var reader = new BinaryReader(stream))
            {
                var header = SatisfactorySaveSerializer.DeserializeHeader(reader);

                Assert.IsTrue(header.SupportsSessionVisibility);

                // No exception should occur, just poke the getter/setter
                var foo = header.SessionVisibility;
                header.SessionVisibility = foo;
            }

            using (var stream = new MemoryStream(SaveHeaderV4Bytes))
            using (var reader = new BinaryReader(stream))
            {
                var header = SatisfactorySaveSerializer.DeserializeHeader(reader);

                Assert.IsFalse(header.SupportsSessionVisibility);

                Assert.ThrowsException<InvalidOperationException>(() => header.SessionVisibility);
                Assert.ThrowsException<InvalidOperationException>(() => header.SessionVisibility = ESessionVisibility.SV_FriendsOnly);
            }
        }
    }
}