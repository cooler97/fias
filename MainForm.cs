using System;
using System.Data;
using System.Data.Common;
using System.Threading;
using System.Windows.Forms;
using Atechnology.Components;
using Atechnology.DBConnections2;
using Atechnology.ecad.Document.Classes;

namespace fias
{
	public partial class MainForm : Form
	{
		private static string PICKUP = "Самовывоз";
		private static string DELIVERY = "Доставка";
		private static string TABLE_NAME = "orderdelivery";
		
		private Thread sqlThread;
		
		private dbconn db;
		
		private OrderClass orderClass;
		
		private string AOGUID = "";
		
		private int idorderdelivery = 0;
		
		private string address = "";
		
		private string comment = "";
		
		private DateTime deliveryDate = DateTime.Now;
		
		private bool isPickup = false;
		
		public MainForm(OrderClass _orderClass)
		{
			InitializeComponent();
			
			db = dbconn._db;
			
			orderClass = _orderClass;
			
			GetData();
			
			if(String.IsNullOrEmpty(AOGUID) && idorderdelivery > 0)
			{
				isPickup = true;
				addressComboBox.Enabled = !isPickup;
				deliveryType.Text = PICKUP;
			}
			else
			{
				deliveryType.Text = DELIVERY;
			}
			
			addressComboBox.Text = address;
			
			commentTextBox.Text = comment;
			
			plandate.Value = GetPlandate();
			
		}
		
		void FilterAddress(object filterString)
		{
			Thread.Sleep(500);
			
			string[] fStringArr = (filterString as string).Split(' ');
			
			string cityName = "";
			string regionName = "";
			
			if(fStringArr.Length > 0)
			{
				cityName = fStringArr[0];
			}
			
			if(fStringArr.Length > 1)
			{
				regionName = fStringArr[1];
			}
			
			String q = String.Format("SELECT "
			                         + " "
			                         + " ISNULL(reg.OFFNAME, '') REGION_OFFNAME, "
			                         + " ISNULL(reg.SHORTNAME, '') REGION_SHORTNAME, "
			                         + " ISNULL(area.OFFNAME, '') AREA_OFFNAME, "
			                         + " ISNULL(area.SHORTNAME, '') AREA_SHORTNAME, "
			                         + " ISNULL(a1.OFFNAME, '') TOWN_OFFNAME, "
			                         + " ISNULL(a1.SHORTNAME, '') TOWN_SHORTNAME, "
			                         + " a1.AOGUID TOWN_AOGUID, "
			                         + " a1.AOLEVEL TOWN_AOLEVEL "
			                         + "  "
			                         + "FROM "
			                         + "	(SELECT "
			                         + "	OFFNAME, "
			                         + "	SHORTNAME, "
			                         + "	PARENTGUID, "
			                         + "	AOGUID, "
			                         + "	AOLEVEL "
			                         + "  FROM "
			                         + "	dbo.addressclassificator "
			                         + "   WHERE "
			                         + "	OFFNAME LIKE '{0}%' AND AOLEVEL = 6 "
			                         + "	UNION ALL "
			                         + "	SELECT "
			                         + "	OFFNAME, "
			                         + "	SHORTNAME, "
			                         + "	PARENTGUID, "
			                         + "	AOGUID, "
			                         + "	AOLEVEL "
			                         + "  FROM "
			                         + "	dbo.addressclassificator "
			                         + "   WHERE "
			                         + "	OFFNAME LIKE '{0}%' AND AOLEVEL = 4 "
			                         + "  ) a1 LEFT JOIN "
			                         + "	dbo.addressclassificator area ON a1.PARENTGUID = area.AOGUID AND area.AOLEVEL IN (2,3) LEFT JOIN "
			                         + "	dbo.addressclassificator reg ON IIF(area.PARENTGUID IS NULL, a1.PARENTGUID, area.PARENTGUID) = reg.AOGUID AND reg.AOLEVEL = 1 "
			                         + "	 "
			                         + "WHERE "
			                         + "	reg.OFFNAME LIKE '{1}%' "
			                         + " "
			                         + "ORDER BY a1.AOLEVEL, reg.OFFNAME, area.OFFNAME, a1.OFFNAME",
			                         cityName, regionName
			                        );
			
			try
			{
				
				db.command.CommandText = q;
				
				db.OpenDB();
				
				DbDataReader reader = db.command.ExecuteReader();

				string addr = "";
				
				string region = "";
				string regionShortName = "";
				
				string area = "";
				string areaShortName = "";
				
				string town = "";
				string townShortName = "";
				
				string aoguid = "";
				
				ComboboxItem item;
				
				while(reader.Read())
				{
					region = reader.GetString(0);
					regionShortName = reader.GetString(1);
					
					area = reader.GetString(2);
					areaShortName = reader.GetString(3);
					
					town = reader.GetString(4);
					townShortName= reader.GetString(5);
					
					aoguid = reader.GetGuid(6).ToString();
					
					if(String.IsNullOrEmpty(area))
					{
						addr = String.Format("{0} {1}, {2} {3}", region, regionShortName, townShortName, town);
					}
					else
					{
						addr = String.Format("{0} {1}, {2} {3}, {4} {5}", region, regionShortName, area, areaShortName, townShortName, town);
					}
					
					item = new ComboboxItem();
					
					item.Text = addr;
					item.Value = aoguid;
					
					if(addressComboBox.InvokeRequired)
					{
						addressComboBox.Invoke(new Action<ComboboxItem>((i) => addressComboBox.Items.Add(i)), item);
					}
				}
			}
			catch(Exception e)
			{
				MessageBox.Show(e.ToString());
			}
			finally
			{
				db.CloseDB();
			}
		}
		
		private void SetCaretToEnd()
		{
			addressComboBox.SelectionStart = addressComboBox.Text.Length;
			addressComboBox.SelectionLength = 0;
		}
		
		void ComboBox1KeyUp(object sender, KeyEventArgs e)
		{
			if(e.KeyCode == Keys.Up || e.KeyCode == Keys.Down
			   || e.KeyCode == Keys.Left || e.KeyCode == Keys.Right)
			{
				return;
			}
			
			if(addressComboBox.SelectedItem != null)
			{
				return;
			}
			
			if(addressComboBox.Text.Length < 3)
			{
				addressComboBox.Items.Clear();
				SetCaretToEnd();
				return;
			}
			string filterText = addressComboBox.Text;
			
			addressComboBox.Items.Clear();
			
			try
			{
				
				if(sqlThread != null && sqlThread.IsAlive)
				{
					sqlThread.Abort();
				}
				
				sqlThread = new Thread(new ParameterizedThreadStart(FilterAddress));
				
				sqlThread.Start(filterText);
				
			}
			catch (ThreadAbortException ex)
			{

			}
			finally
			{
				db.CloseDB();
			}
			
			addressComboBox.Text = filterText;

			SetCaretToEnd();
			
			addressComboBox.DroppedDown = true;
			Cursor.Current = Cursors.Default;
		}
		
		void CloseBtnClick(object sender, EventArgs e)
		{
			this.DialogResult = DialogResult.Cancel;
			this.Close();
		}
		
		void AcceptBtnClick(object sender, EventArgs e)
		{
			if(orderClass == null)
			{
				this.Close();
				return;
			}
			
			if(deliveryType.Text == string.Empty)
			{
				AtMessageBox.Show("Выберите тип доставки!");
				return;
			}
			
			string aoguid = "NULL";
			string aotext = "";
			
			SetPlanDate();
			
			ComboboxItem item = addressComboBox.SelectedItem as ComboboxItem;
			
			if(isPickup)
			{
				aotext = PICKUP;
			}
			else if(idorderdelivery == 0 && item == null)
			{
				AtMessageBox.Show("Укажите пункт доставки!");
				return;
			}
			else if (item != null)
			{
				aoguid = "'" + (string)item.Value + "'";
				aotext = item.Text;
			}
			else if (idorderdelivery > 0)
			{
				aoguid = "'" + AOGUID + "'";
				aotext = address;
			}
			else
			{
				AtMessageBox.Show("Неизвестная ошибка!");
				return;
			}
			
			if(idorderdelivery == 0)
			{
				idorderdelivery = dbconn.GetGenId("gen_" + TABLE_NAME);
				
				db.command.CommandText = String.Format("INSERT INTO {0} (idorderdelivery, idorder, idaddress, address, comment) "
				                                       + " VALUES ({1},{2},{3},'{4}','{5}')", TABLE_NAME,
				                                       idorderdelivery,
				                                       orderClass.idorder,
				                                       aoguid,
				                                       aotext,
				                                       commentTextBox.Text);

			}
			else
			{
				db.command.CommandText = String.Format("UPDATE {0} SET idaddress = {1}, address = '{2}', comment = '{3}' "
				                                       + " WHERE idorderdelivery = {4}", TABLE_NAME,
				                                       aoguid,
				                                       aotext,
				                                       commentTextBox.Text,
				                                       idorderdelivery);
			}
			
//			MessageBox.Show(db.command.CommandText);
			
			try
			{
				db.OpenDB();
				db.command.ExecuteNonQuery();
				orderClass.OrderItemForm.buttonEditDestanation.Text = aotext;
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.ToString());
			}
			finally
			{
				db.CloseDB();
			}
			
			this.Close();
		}
		
		void GetData()
		{
			db.command.CommandText = String.Format("SELECT TOP 1"
			                                       + " idorderdelivery, "
			                                       + " address, "
			                                       + " idaddress, "
			                                       + " comment "
			                                       + "FROM "
			                                       + " orderdelivery "
			                                       + "WHERE "
			                                       + " deleted IS NULL AND "
			                                       + " idorder = {0}",
			                                       orderClass.idorder);			
			try
			{				
				db.OpenDB();
				
				DbDataReader reader = db.command.ExecuteReader();
				
				if (reader.HasRows)
				{
					while (reader.Read())
					{
						idorderdelivery = reader.GetInt32(0);
						address = reader.IsDBNull(1) ? "" : reader.GetString(1);
						AOGUID = reader.IsDBNull(2) ? "" : reader.GetGuid(2).ToString();
						comment = reader.IsDBNull(3) ? "" : reader.GetString(3);
					}
				}
				
				reader.Close();
				
			}
			catch (Exception e)
			{
				MessageBox.Show(e.ToString());
			}
			finally
			{
				db.CloseDB();
			}			
			
		}
		
		void SetPlanDate()
		{
			DataRow row = GetDeliveryRow();
			
			if(row != null)
			{
				row["plandate"] = plandate.Value.Date;
//				row["comment"] = "Дата установлена в ручную";
			}
		}
		
		DateTime GetPlandate()
		{
			DateTime result = DateTime.Now;
			
			DataRow row = GetDeliveryRow();
			
			if(row != null)
			{
				result = row["plandate"] != DBNull.Value ? (DateTime)row["plandate"] : DateTime.Now;
			}
			
			return result;
		}
		
		DataRow GetDeliveryRow()
		{
			DataRow row = null;
			
			foreach(DataRow dr in orderClass.ds.orderdiraction.Rows)
			{

				if((string)dr["diraction_name"] != DELIVERY)
					continue;
				
				row = dr;
			}
			
			return row;
		}
		
		void DeliveryTypeSelectedIndexChanged(object sender, EventArgs e)
		{
			isPickup = deliveryType.Text == PICKUP;
			addressComboBox.Enabled = !isPickup;
		}
	}
}
