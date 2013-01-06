using System;
using System.Linq;
using System.Media;
using System.Windows.Forms;

namespace WowMoPObjMgrTest
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //new DescriptorsDumper();

            listView1.Items.Clear();

            int total = 0;
            int items = 0;
            int units = 0;
            int gos = 0;
            int other = 0;

            foreach (WowObject obj in Game.ObjMgr)
            {
                total++;

                if (obj.IsA(ObjectTypeFlags.Item))
                    items++;
                else if (obj.IsA(ObjectTypeFlags.Unit))
                    units++;
                else if (obj.IsA(ObjectTypeFlags.GameObject))
                    gos++;
                else
                    other++;

                //if (obj.Guid == objMgr.ActivePlayer)
                //{
                //    MessageBox.Show("Me!");
                //}

                //if (obj.Guid != objMgr.ActivePlayer)
                //    continue;

                listView1.Items.Add(new ListViewItem(new string[]
                    {
                        obj.Pointer.ToInt64().ToString("X16"),
                        obj.Type.ToString(),
                        obj.VisibleGuid.ToString("X16"),
                        obj.Entry.ToString(),
                        obj.Scale.ToString(),
                        GetObjInfo(obj)
                    }));

                //obj.SetValue(UnitFields.Health, obj.GetValue<int>(UnitFields.MaxHealth));
                //obj.SetValue(UnitFields.Level, 90);
                //obj.SetValue(UnitFields.Target, obj.Guid);
                //obj.SetValue(ObjectFields.Scale, 5.0f);
            }

            label1.Text = total.ToString();
            label4.Text = units.ToString();
            label6.Text = gos.ToString();
            label8.Text = items.ToString();
            label10.Text = other.ToString();
        }

        string GetObjInfo(WowObject obj)
        {
            if (obj.Type == WowObjectType.Container)
                return "Num. slots: " + obj.GetValue<int>(CGContainerData.NumSlots).ToString();

            if (obj.Guid == Game.ObjMgr.ActivePlayer)
                return "<<< Me!";
                //return Memory.Read<Vector3>(obj.Pointer + 0x7E0 /* position x86 */).ToString();

            if (obj.Type == WowObjectType.Player)
                return "Home Realm: " + obj.GetValue<int>(CGPlayerData.homePlayerRealm).ToString("X8");

            return String.Empty;
        }

        int[] npcs = new int[] { 52176, 54318, 54319, 50831 };

        SoundPlayer sp = new SoundPlayer("RaidWarning.wav");

        private void timer1_Tick(object sender, EventArgs e)
        {
            WowUnit me = Game.ObjMgr.ActivePlayerObj;

            if (me == null)
                return;

            WowUnit target = (WowUnit)Game.ObjMgr[me.GetValue<ulong>(CGUnitData.Target)];

            if (target == null)
                return;

            label11.Text = target.UnitReaction.ToString();

            //var targets = objMgr.Where(o => o.Type == WowObjectType.Unit && npcs.Contains(o.Entry));

            //if (targets.Any())
            //{
            //    foreach (var o in targets)
            //    {
            //        if (o.GetValue<int>(UnitFields.Health) > 0)
            //            sp.PlayLooping();
            //        else
            //            sp.Stop();
            //    }
            //}

            //if (objMgr.Any(o => o.Type == WowObjectType.Unit && npcs.Contains(o.Entry) && o.GetValue<int>(UnitFields.Health) > 0))
            //{
            //    sp.PlayLooping();
            //}
            //else
            //{
            //    sp.Stop();
            //}

            // Rare units tracking
            foreach (WowObject obj in Game.ObjMgr)
            {
                if (obj.Type == WowObjectType.Unit)
                {
                    if (npcs.Contains(obj.Entry))
                    {
                        if (obj.GetValue<int>(CGUnitData.Health) > 0)
                            sp.PlayLooping();
                        else
                            sp.Stop();
                    }
                }
            }
        }
    }
}
