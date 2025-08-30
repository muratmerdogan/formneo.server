using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using vesa.core.Models;

namespace vesa.repository.Seeds
{
	internal class EmploWorkFlowDefinationsSeedyeeSeed : IEntityTypeConfiguration<WorkFlowDefination>
	{
		private static string jsonData = @"{
	""nodes"": [
		{
			""id"": ""3802a094-9a5a-4d0c-9fd3-8f520d23037e"",
			""name"": ""Start"",
			""description"": """",
			""nodeClazz"": ""StartNode"",
			""configuration"": {
				""outputCount"": 3
			},
			""properties"": {
				""left"": 220,
				""top"": 60,
				""icon"": """"
			}
		},
		{
			""id"": ""9d36b611-b373-4ae5-a3af-b5e8e8e6af3d"",
			""name"": ""Onay Nesnesi"",
			""description"": """",
			""nodeClazz"": ""ApproverNode"",
			""configuration"": {
				""routes"": [
					""Onay"",
					""Red""
				],
				""approverselect"": ""STATICUSER"",
				""staticuser"": ""mmerdogan""
			},
			""properties"": {
				""left"": -80,
				""top"": 280,
				""icon"": """"
			}
		},
		{
			""id"": ""76185685-8d25-4e56-a2cd-283c97d6cc65"",
			""name"": ""Onay Nesnesi"",
			""description"": """",
			""nodeClazz"": ""ApproverNode"",
			""configuration"": {
				""routes"": [
					""Onay"",
					""Red""
				]
			},
			""properties"": {
				""left"": 220,
				""top"": 280,
				""icon"": """"
			}
		},
		{
			""id"": ""b8b0f481-43c4-497d-a7ee-2d8d5376d2ab"",
			""name"": ""End"",
			""description"": """",
			""nodeClazz"": ""StopNode"",
			""configuration"": {},
			""properties"": {
				""left"": 220,
				""top"": 620,
				""icon"": """"
			}
		},
		{
			""id"": ""81143eab-2663-45ff-a177-f3fb50378acd"",
			""name"": ""Email"",
			""description"": """",
			""nodeClazz"": ""EmailNode"",
			""configuration"": {
				""subjectTemplate"": ""Temperature high!"",
				""receiversTemplate"": ""user.name@example.com"",
				""textTemplate"": ""Temperature is {{data.measurement.temperature}}"",
				""resultPath"": ""mail""
			},
			""properties"": {
				""left"": 500,
				""top"": 280,
				""icon"": """"
			}
		},
		{
			""id"": ""1d28c63f-0c71-4e8f-9b26-4a99c486fde1"",
			""name"": ""Email"",
			""description"": """",
			""nodeClazz"": ""EmailNode"",
			""configuration"": {
				""subjectTemplate"": ""Temperature high!"",
				""receiversTemplate"": ""user.name@example.com"",
				""textTemplate"": ""Temperature is {{data.measurement.temperature}}"",
				""resultPath"": ""mail""
			},
			""properties"": {
				""left"": 500,
				""top"": 340,
				""icon"": """"
			}
		},
		{
			""id"": ""525eeaa8-a465-4476-a491-952fe9452dc7"",
			""name"": ""Email"",
			""description"": """",
			""nodeClazz"": ""EmailNode"",
			""configuration"": {
				""subjectTemplate"": ""Temperature high!"",
				""receiversTemplate"": ""user.name@example.com"",
				""textTemplate"": ""Temperature is {{data.measurement.temperature}}"",
				""resultPath"": ""mail""
			},
			""properties"": {
				""left"": 500,
				""top"": 400,
				""icon"": """"
			}
		},
		{
			""id"": ""0f045ecd-9004-4e39-8af9-dc923d9c6e7c"",
			""name"": ""Debug"",
			""description"": """",
			""nodeClazz"": ""DebugNode"",
			""configuration"": {
				""loggingLevel"": ""INFO"",
				""messageTemplate"": null
			},
			""properties"": {
				""left"": -40,
				""top"": 520,
				""icon"": """"
			}
		}
	],
	""links"": [
		{
			""fromNode"": ""3802a094-9a5a-4d0c-9fd3-8f520d23037e"",
			""fromPort"": ""defaultFromPort"",
			""toNode"": ""9d36b611-b373-4ae5-a3af-b5e8e8e6af3d"",
			""properties"": {
				""left"": 170.5,
				""top"": 190
			}
		},
		{
			""fromNode"": ""3802a094-9a5a-4d0c-9fd3-8f520d23037e"",
			""fromPort"": ""defaultFromPort"",
			""toNode"": ""76185685-8d25-4e56-a2cd-283c97d6cc65"",
			""properties"": {
				""left"": 320.5,
				""top"": 190
			}
		},
		{
			""fromNode"": ""3802a094-9a5a-4d0c-9fd3-8f520d23037e"",
			""fromPort"": ""defaultFromPort"",
			""toNode"": ""81143eab-2663-45ff-a177-f3fb50378acd"",
			""properties"": {
				""left"": 460,
				""top"": 190
			}
		},
		{
			""fromNode"": ""81143eab-2663-45ff-a177-f3fb50378acd"",
			""fromPort"": ""defaultFromPort"",
			""toNode"": ""1d28c63f-0c71-4e8f-9b26-4a99c486fde1"",
			""properties"": {
				""left"": 600,
				""top"": 330
			}
		},
		{
			""fromNode"": ""1d28c63f-0c71-4e8f-9b26-4a99c486fde1"",
			""fromPort"": ""defaultFromPort"",
			""toNode"": ""525eeaa8-a465-4476-a491-952fe9452dc7"",
			""properties"": {
				""left"": 600,
				""top"": 390
			}
		},
		{
			""fromNode"": ""525eeaa8-a465-4476-a491-952fe9452dc7"",
			""fromPort"": ""defaultFromPort"",
			""toNode"": ""b8b0f481-43c4-497d-a7ee-2d8d5376d2ab"",
			""properties"": {
				""left"": 460,
				""top"": 530
			}
		},
		{
			""fromNode"": ""9d36b611-b373-4ae5-a3af-b5e8e8e6af3d"",
			""fromPort"": ""Onay"",
			""toNode"": ""0f045ecd-9004-4e39-8af9-dc923d9c6e7c"",
			""properties"": {
				""left"": 20.5,
				""top"": 440.5
			}
		},
		{
			""fromNode"": ""9d36b611-b373-4ae5-a3af-b5e8e8e6af3d"",
			""fromPort"": ""Red"",
			""toNode"": ""b8b0f481-43c4-497d-a7ee-2d8d5376d2ab"",
			""properties"": {
				""left"": 190.5,
				""top"": 490.5
			}
		}
	],
	""properties"": {
		""viewportTransform"": [
			0.5,
			0,
			0,
			0.5,
			351.5585937500001,
			147.8125
		]
	}
}";
		public void Configure(EntityTypeBuilder<WorkFlowDefination> builder)
		{

			//builder.HasData(
			//	new WorkFlowDefination { Client = "00", Company = "01", Plant = "01", Revision = 0, IsActive = true, CreatedDate = DateTime.Now, Id = Guid.NewGuid(), WorkflowName = "Onay Basic", FormId="1212"  Defination = jsonData });



			//builder.HasData(
			//   new Departments { Guid = Guid.NewGuid().ToString(), Client = "00", Company = "01", Plant = "01", DepartmentId = "1",  DepartmentText = "Yönetim" });


		}
	}
}
