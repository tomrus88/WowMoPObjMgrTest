using System;
using System.Collections;
using System.Diagnostics;
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
            //Stopwatch sw = new Stopwatch();

            //sw.Start();

            //for (int i = 0; i < 50000; ++i)
            //{
            //    var db = Game.g_FactionTemplateDB;
            //    foreach (var f in db)
            //    {
            //    }
            //}

            //sw.Stop();

            //var passed1 = sw.ElapsedMilliseconds;
            //MessageBox.Show(passed1.ToString());

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

                if (obj.Type != WowObjectType.Player)
                    continue;

                //if (obj.Guid != Game.ObjMgr.ActivePlayer)
                //    continue;

                ListViewItem itm = listView1.Items.Add(new ListViewItem(new string[]
                    {
                        obj.Pointer.ToInt64().ToString("X16"),
                        obj.Type.ToString(),
                        obj.VisibleGuid.ToString("X16"),
                        obj.Entry.ToString(),
                        obj.Scale.ToString(),
                        GetObjInfo(obj)
                    }));

                itm.Tag = obj.Guid;
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
                return "Num. slots: " + (obj as WowContainer).NumSlots.ToString();

            if (obj.Guid == Game.ObjMgr.ActivePlayer)
                return "<<< Me!";
            //return Memory.Read<Vector3>(obj.Pointer + 0x7E0 /* position x86 */).ToString();

            if (obj.Type == WowObjectType.Player)
                return "Home Realm: " + (obj as WowPlayer).RealmId.ToString("X8");

            if (obj.Type == WowObjectType.Unit && (obj as WowUnit).IsPet)
                return "Pet";

            return String.Empty;
        }

        int[] npcs = new int[] { 52176, 54318, 54319, 50831 };

        SoundPlayer sp = new SoundPlayer("RaidWarning.wav");

        private void timer1_Tick(object sender, EventArgs e)
        {
            WowUnit me = Game.ObjMgr.ActivePlayerObj;

            if (me == null)
                return;

            WowUnit target = me.Target;

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

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count == 0)
                return;

            WowObject obj = Game.ObjMgr[(ulong)listView1.SelectedItems[0].Tag];

            if (obj == null)
                return;

            propertyGrid1.SelectedObject = obj;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            DBC<SpellMiscRec> SpellMisc = new DBC<SpellMiscRec>(Memory.BaseAddress + (IntPtr.Size == 4 ? 0xBFDA68 : 0 /* cba searching x64 offset*/), false);

            int removeValue = Convert.ToInt32(textBox1.Text, 16);
            int addValue = Convert.ToInt32(textBox2.Text, 16);
            int idx = Convert.ToInt32(textBox3.Text);

            if (idx < 0 || idx > 12)
            {
                MessageBox.Show("Incorrect attribute index (0...12)");
                return;
            }

            for (int i = SpellMisc.MinIndex; i <= SpellMisc.MaxIndex; ++i)
            {
                IntPtr row = SpellMisc.GetRowPtr(i);

                if (row != IntPtr.Zero)
                {
                    int attr0 = Memory.Read<int>(row + 12 + idx * 4);
                    attr0 &= ~removeValue;
                    attr0 |= addValue;
                    Memory.Write<int>(row + 12 + idx * 4, attr0);
                }
            }

            MessageBox.Show("Done!");
        }
    }
}
