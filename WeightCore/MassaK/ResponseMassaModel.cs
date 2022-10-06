// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Linq;

namespace WeightCore.MassaK;

public class ResponseMassaModel
{
	public int Weight;
	public int ScaleFactor = 1_000;
	public byte _division;
	public byte Division
	{
		get => _division;
		set
		{
			_division = value;
			ScaleFactor = value switch
			{
				0x00 => 10000,
				0x01 => 1000,
				0x02 => 100,
				0x03 => 10,
				0x04 => 1,
				_ => 1_000,
			};
		}
	}
	public byte IsStable;
	public byte Net;
	public byte Zero;
	public int Tare;

	public ResponseMassaModel(byte[] response)
	{
		if (response is null || response.Length < 10)
			return;
		Weight = BitConverter.ToInt32(response.Skip(6).Take(4).ToArray(), 0);
		Division = response[10];
		IsStable = response[11];
		Net = response[12];
		Zero = response[13];
		Tare = BitConverter.ToInt32(response.Skip(14).Take(4).ToArray(), 0);
	}
}