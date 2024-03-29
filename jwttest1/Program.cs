﻿using JWT;
using JWT.Algorithms;
using JWT.Serializers;
using System;
using System.Collections.Generic;

namespace jwttest1
{
    class Program
    {
        static void Main(string[] args)
        {

            var exp = ((DateTime.UtcNow.AddSeconds(30) - DateTime.Parse("1970-01-01 00:00:00")).Ticks) / 10000000;

            var payload = new Dictionary<string, object> {
                { "UserId", 123 },
                { "UserName", "admin" },
                { "exp",exp }
            };


            var secret = "GQDstcKsx0NHjPOuXOYg5MbeJ1XT0uFiwDVvVBrk";//不要泄露（这是服务器端秘钥）
            IJwtAlgorithm algorithm = new HMACSHA256Algorithm();
            IJsonSerializer serializer = new JsonNetSerializer();
            IBase64UrlEncoder urlEncoder = new JwtBase64UrlEncoder();
            IJwtEncoder encoder = new JwtEncoder(algorithm, serializer, urlEncoder);
            var token = encoder.Encode(payload, secret);

            //IJsonSerializer serializer = new JsonNetSerializer();
            IDateTimeProvider provider = new UtcDateTimeProvider();
            IJwtValidator validator = new JwtValidator(serializer, provider);
            //IBase64UrlEncoder urlEncoder = new JwtBase64UrlEncoder();
            IJwtDecoder decoder = new JwtDecoder(serializer, validator, urlEncoder);
            var json = decoder.Decode(token, secret, verify: true);

            Console.WriteLine(json);

            Console.ReadKey();
        }
    }
}
