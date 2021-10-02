using Microsoft.Band;
using Microsoft.Band.Tiles;
using Microsoft.Band.Tiles.Pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Windows.Storage;
using Windows.Storage.Streams;
using Windows.UI.Xaml.Media.Imaging;

namespace BandCalc.WindowsApp
{
	internal class Functions
	{
		private readonly PageLayout pageLayout;
		private readonly PageLayoutData pageLayoutData;
		
		private readonly FlowPanel panel = new FlowPanel();
		private readonly FlowPanel panel2 = new FlowPanel();
		private readonly TextButton button = new TextButton();
		private readonly TextButton button2 = new TextButton();
		private readonly TextButton button3 = new TextButton();
		private readonly FlowPanel panel3 = new FlowPanel();
		private readonly TextButton button4 = new TextButton();
		private readonly TextButton button5 = new TextButton();
		private readonly TextButton button6 = new TextButton();
		private readonly FlowPanel panel4 = new FlowPanel();
		private readonly TextButton button7 = new TextButton();
		
		private readonly TextButtonData buttonData = new TextButtonData(21, "sqrt");
		private readonly TextButtonData button2Data = new TextButtonData(22, "sin");
		private readonly TextButtonData button3Data = new TextButtonData(23, "cos");
		private readonly TextButtonData button4Data = new TextButtonData(24, "pi");
		private readonly TextButtonData button5Data = new TextButtonData(25, "e");
		private readonly TextButtonData button6Data = new TextButtonData(26, "exp");
		private readonly TextButtonData button7Data = new TextButtonData(27, "delete");
		
		public Functions()
		{
			LoadIconMethod = LoadIcon;
			AdjustUriMethod = (uri) => uri;
			
			panel = new FlowPanel();
			panel.Orientation = FlowPanelOrientation.Vertical;
			panel.Rect = new PageRect(0, 0, 248, 128);
			panel.ElementId = 100;
			panel.Margins = new Margins(0, 0, 0, 0);
			panel.HorizontalAlignment = HorizontalAlignment.Left;
			panel.VerticalAlignment = VerticalAlignment.Top;
			
			panel2 = new FlowPanel();
			panel2.Orientation = FlowPanelOrientation.Horizontal;
			panel2.Rect = new PageRect(0, 0, 240, 45);
			panel2.ElementId = 99;
			panel2.Margins = new Margins(0, 0, 0, 0);
			panel2.HorizontalAlignment = HorizontalAlignment.Left;
			panel2.VerticalAlignment = VerticalAlignment.Top;
			
			button = new TextButton();
			button.PressedColor = new BandColor(0, 121, 214);
			button.Rect = new PageRect(0, 0, 55, 35);
			button.ElementId = 21;
			button.Margins = new Margins(30, 0, 0, 0);
			button.HorizontalAlignment = HorizontalAlignment.Center;
			button.VerticalAlignment = VerticalAlignment.Top;
			
			panel2.Elements.Add(button);
			
			button2 = new TextButton();
			button2.PressedColor = new BandColor(0, 121, 214);
			button2.Rect = new PageRect(0, 0, 55, 35);
			button2.ElementId = 22;
			button2.Margins = new Margins(10, 0, 0, 0);
			button2.HorizontalAlignment = HorizontalAlignment.Center;
			button2.VerticalAlignment = VerticalAlignment.Top;
			
			panel2.Elements.Add(button2);
			
			button3 = new TextButton();
			button3.PressedColor = new BandColor(0, 121, 214);
			button3.Rect = new PageRect(0, 0, 55, 35);
			button3.ElementId = 23;
			button3.Margins = new Margins(10, 0, 0, 0);
			button3.HorizontalAlignment = HorizontalAlignment.Center;
			button3.VerticalAlignment = VerticalAlignment.Top;
			
			panel2.Elements.Add(button3);
			
			panel.Elements.Add(panel2);
			
			panel3 = new FlowPanel();
			panel3.Orientation = FlowPanelOrientation.Horizontal;
			panel3.Rect = new PageRect(0, 0, 240, 45);
			panel3.ElementId = 98;
			panel3.Margins = new Margins(0, 0, 0, 0);
			panel3.HorizontalAlignment = HorizontalAlignment.Left;
			panel3.VerticalAlignment = VerticalAlignment.Top;
			
			button4 = new TextButton();
			button4.PressedColor = new BandColor(0, 121, 214);
			button4.Rect = new PageRect(0, 0, 55, 35);
			button4.ElementId = 24;
			button4.Margins = new Margins(30, 0, 0, 0);
			button4.HorizontalAlignment = HorizontalAlignment.Center;
			button4.VerticalAlignment = VerticalAlignment.Top;
			
			panel3.Elements.Add(button4);
			
			button5 = new TextButton();
			button5.PressedColor = new BandColor(0, 121, 214);
			button5.Rect = new PageRect(0, 0, 55, 35);
			button5.ElementId = 25;
			button5.Margins = new Margins(10, 0, 0, 0);
			button5.HorizontalAlignment = HorizontalAlignment.Center;
			button5.VerticalAlignment = VerticalAlignment.Top;
			
			panel3.Elements.Add(button5);
			
			button6 = new TextButton();
			button6.PressedColor = new BandColor(0, 121, 214);
			button6.Rect = new PageRect(0, 0, 55, 35);
			button6.ElementId = 26;
			button6.Margins = new Margins(10, 0, 0, 0);
			button6.HorizontalAlignment = HorizontalAlignment.Center;
			button6.VerticalAlignment = VerticalAlignment.Top;
			
			panel3.Elements.Add(button6);
			
			panel.Elements.Add(panel3);
			
			panel4 = new FlowPanel();
			panel4.Orientation = FlowPanelOrientation.Horizontal;
			panel4.Rect = new PageRect(0, 0, 240, 45);
			panel4.ElementId = 97;
			panel4.Margins = new Margins(0, 0, 0, 0);
			panel4.HorizontalAlignment = HorizontalAlignment.Left;
			panel4.VerticalAlignment = VerticalAlignment.Top;
			
			button7 = new TextButton();
			button7.PressedColor = new BandColor(0, 121, 214);
			button7.Rect = new PageRect(0, 0, 185, 35);
			button7.ElementId = 27;
			button7.Margins = new Margins(30, 0, 0, 0);
			button7.HorizontalAlignment = HorizontalAlignment.Center;
			button7.VerticalAlignment = VerticalAlignment.Top;
			
			panel4.Elements.Add(button7);
			
			panel.Elements.Add(panel4);
			pageLayout = new PageLayout(panel);
			
			PageElementData[] pageElementDataArray = new PageElementData[7];
			pageElementDataArray[0] = buttonData;
			pageElementDataArray[1] = button2Data;
			pageElementDataArray[2] = button3Data;
			pageElementDataArray[3] = button4Data;
			pageElementDataArray[4] = button5Data;
			pageElementDataArray[5] = button6Data;
			pageElementDataArray[6] = button7Data;
			
			pageLayoutData = new PageLayoutData(pageElementDataArray);
		}
		
		public PageLayout Layout
		{
			get
			{
				return pageLayout;
			}
		}
		
		public PageLayoutData Data
		{
			get
			{
				return pageLayoutData;
			}
		}
		
		public Func<string, Task<BandIcon>> LoadIconMethod
		{
			get;
			set;
		}
		
		public Func<string, string> AdjustUriMethod
		{
			get;
			set;
		}
		
		private static async Task<BandIcon> LoadIcon(string uri)
		{
			StorageFile imageFile = await StorageFile.GetFileFromApplicationUriAsync(new Uri(uri));
			
			using (IRandomAccessStream fileStream = await imageFile.OpenAsync(FileAccessMode.Read))
			{
				WriteableBitmap bitmap = new WriteableBitmap(1, 1);
				await bitmap.SetSourceAsync(fileStream);
				return bitmap.ToBandIcon();
			}
		}
		
		public async Task LoadIconsAsync(BandTile tile)
		{
			await Task.Run(() => { }); // Dealing with CS1998
		}
		
		public static BandTheme GetBandTheme()
		{
			var theme = new BandTheme();
			theme.Base = new BandColor(51, 102, 204);
			theme.HighContrast = new BandColor(58, 120, 221);
			theme.Highlight = new BandColor(58, 120, 221);
			theme.Lowlight = new BandColor(49, 101, 186);
			theme.Muted = new BandColor(43, 90, 165);
			theme.SecondaryText = new BandColor(137, 151, 171);
			return theme;
		}
		
		public static BandTheme GetTileTheme()
		{
			var theme = new BandTheme();
			theme.Base = new BandColor(51, 102, 204);
			theme.HighContrast = new BandColor(58, 120, 221);
			theme.Highlight = new BandColor(58, 120, 221);
			theme.Lowlight = new BandColor(49, 101, 186);
			theme.Muted = new BandColor(43, 90, 165);
			theme.SecondaryText = new BandColor(137, 151, 171);
			return theme;
		}
		
		public class PageLayoutData
		{
			private readonly PageElementData[] array;
			
			public PageLayoutData(PageElementData[] pageElementDataArray)
			{
				array = pageElementDataArray;
			}
			
			public int Count
			{
				get
				{
					return array.Length;
				}
			}
			
			public T Get<T>(int i) where T : PageElementData
			{
				return (T)array[i];
			}
			
			public T ById<T>(short id) where T:PageElementData
			{
				return (T)array.FirstOrDefault(elm => elm.ElementId == id);
			}
			
			public PageElementData[] All
			{
				get
				{
					return array;
				}
			}
		}
		
	}
}
