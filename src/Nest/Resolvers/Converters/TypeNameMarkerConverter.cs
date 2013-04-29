﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;


namespace Nest.Resolvers.Converters
{
	public class TypeNameMarkerConverter : JsonConverter
	{
		private readonly IConnectionSettings _connectionSettings;

		public TypeNameMarkerConverter(IConnectionSettings connectionSettings)
		{
			this._connectionSettings = connectionSettings;
		}

		public override bool CanConvert(Type objectType)
		{
			return typeof(TypeNameMarker) == objectType;
		}

		public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
		{
			var marker = value as TypeNameMarker;
			if (marker == null)
			{
				writer.WriteNull();
				return;
			}

			var typeName = marker.Resolve(this._connectionSettings);
			writer.WriteValue(typeName);
		}

		public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
		{
			if (reader.TokenType == JsonToken.String)
			{
				string typeName = reader.Value.ToString();
				return (TypeNameMarker) typeName;
			}
			return null;
		}

	}
}