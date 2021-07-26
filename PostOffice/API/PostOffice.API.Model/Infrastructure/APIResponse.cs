﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PostOffice.API.Model.Infrastructure
{
	public class APIResponse<T>
	{
		public T Data { get; set; }
		public string Error { get; set; }
		public bool IsSuccess { get; set; }
	}

	public class APIResponse
	{
		public string Error { get; set; }
		public bool IsSuccess { get; set; }
	}
}
