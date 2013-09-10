using System;
using System.Diagnostics;
using System.Linq;
using System.Media;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace WowMoPObjMgrTest
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            //int size = Marshal.SizeOf(typeof(CurMgr));

            //var objs = Game.ObjMgr.Where(o => o.Type == WowObjectType.GameObject).OrderBy(o => (o as WowGameObject).DistanceToMe);
            //foreach (WowGameObject o in objs)
            //{
            //    Console.WriteLine("{0}: {1}", o.Entry, o.GetValue<int>(CGGameObjectData.DisplayID));
            //}

            //var pl = Game.ObjMgr.Where(o => o.Type == WowObjectType.GameObject).First();
            //var vt = Memory.Read<IntPtr>(pl.Pointer);
            //if (pl != null)
            //{
            //    for (var i = 0; i < 0x1200; i += 4)
            //        Console.WriteLine("{0:X4}: {1}", i, Memory.Read<float>(pl.Pointer + i));
            //}

            InitializeComponent();
        }

        //int bitIndex = 1;

        private void button1_Click(object sender, EventArgs e)
        {
            //Stopwatch sw = new Stopwatch();

            //sw.Start();

            //for (int i = 0; i < 50000; ++i)
            //{
            //    var db = Game.FactionTemplateDB;
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
            int conts = 0;
            int units = 0;
            int plrs = 0;
            int gos = 0;
            int other = 0;

            //var objects = Game.ObjMgr.Where(o => o.Type == WowObjectType.GameObject).OrderBy(o => (o as WowGameObject).DistanceToMe);
            var objects = Game.ObjMgr;

            foreach (WowObject obj in objects)
            {
                //if (obj.Entry == 223103)
                //{
                //    if (bitIndex == 1)
                //        obj.DynamicFlags = 0;

                //    obj.DynamicFlags = (1u << bitIndex);
                //    bitIndex++;
                //}

                //if (obj.GetValue<int>(CGGameObjectData.DisplayID) == 5744)
                //    obj.SetValue<int>(CGGameObjectData.StateSpellVisualID, 23216);

                total++;

                if (obj.IsA(ObjectTypeFlags.Item))
                {
                    items++;
                    if (obj.IsA(ObjectTypeFlags.Container))
                        conts++;
                }
                else if (obj.IsA(ObjectTypeFlags.Unit))
                {
                    units++;
                    if (obj.IsA(ObjectTypeFlags.Player))
                        plrs++;
                }
                else if (obj.IsA(ObjectTypeFlags.GameObject))
                    gos++;
                else
                    other++;

                //if (obj.Type != WowObjectType.Player)
                //    continue;

                //if (obj.Guid != Game.ObjMgr.ActivePlayer)
                //    continue;

                ListViewItem itm = listView1.Items.Add(new ListViewItem(new string[]
                    {
                        obj.ToString(),
                        obj.Type.ToString(),
                        obj.VisibleGuid.ToString(),
                        obj.Entry.ToString(),
                        obj.Scale.ToString(),
                        GetObjInfo(obj)
                    }));

                itm.Tag = obj.Guid;
            }

            bool showSelf = false;

            if (showSelf)
            {
                var pl = Game.ObjMgr.ActivePlayerObj;

                ListViewItem lvItm = listView1.Items.Add(new ListViewItem(new string[]
                    {
                        pl.ToString(),
                        pl.Type.ToString(),
                        pl.VisibleGuid.ToString(),
                        pl.Entry.ToString(),
                        pl.Scale.ToString(),
                        GetObjInfo(pl)
                    }));

                lvItm.Tag = pl.Guid;
            }

            label1.Text = total.ToString();
            label4.Text = String.Format("{0} ({1} players)", units, plrs);
            label6.Text = gos.ToString();
            label8.Text = String.Format("{0} ({1} containters)", items, conts);
            label10.Text = other.ToString();
        }

        string GetObjInfo(WowObject obj)
        {
            if (obj.Type == WowObjectType.Container)
                return "Num. slots: " + (obj as WowContainer).NumSlots.ToString();

            if (obj.Guid == Game.ObjMgr.ActivePlayer)
                return "<<< Me!" + " " + (obj as WowUnit).Position;

            if (obj.Type == WowObjectType.Player)
                return "Home Realm: " + (obj as WowPlayer).RealmId.ToString("X8");

            if (obj.Type == WowObjectType.Unit && (obj as WowUnit).IsPet)
                return "Pet";

            return String.Empty;
        }

        int[] gos = new int[] { 223103, 223107 };
        int[] npcs = new int[] { /*52176, 54318, 54319, 50831,*/ 50339 };

        SoundPlayer sp = new SoundPlayer("RaidWarning.wav");

        private void timer1_Tick(object sender, EventArgs e)
        {
            //var objects = Game.ObjMgr.Where(o => o.Type == WowObjectType.GameObject && o.GetValue<int>(CGGameObjectData.DisplayID) == 5744);
            //foreach (WowObject obj in objects)
            //{
            //    obj.SetValue<int>(CGGameObjectData.StateSpellVisualID, 23216);
            //}

            //WowUnit me = Game.ObjMgr.ActivePlayerObj;

            //if (me == null)
            //    return;

            //WowUnit target = me.TargetUnit;

            //if (target != null)
            //    label11.Text = target.UnitReaction(me).ToString();

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

            WowObject obj = Game.ObjMgr[(WowGuid)listView1.SelectedItems[0].Tag];

            if (obj == null)
                return;

            propertyGrid1.SelectedObject = obj;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            int removeValue = Convert.ToInt32(textBox1.Text, 16);
            int addValue = Convert.ToInt32(textBox2.Text, 16);
            int idx = Convert.ToInt32(textBox3.Text);

            if (idx < 0 || idx > 12)
            {
                MessageBox.Show("Incorrect attribute index (0...12)");
                return;
            }

            for (int i = Game.SpellMiscDB.MinIndex; i <= Game.SpellMiscDB.MaxIndex; ++i)
            {
                IntPtr row = Game.SpellMiscDB.GetRowPtr(i);

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

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                var val = (TrackCreatureFlags)Convert.ToInt32(textBox4.Text);

                var player = Game.ObjMgr.ActivePlayerObj;

                if (player != null)
                    player.TrackCreatureMask = val;
            }
            catch
            {
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            try
            {
                var val = (TrackObjectFlags)Convert.ToInt32(textBox5.Text);

                var player = Game.ObjMgr.ActivePlayerObj;

                if (player != null)
                    player.TrackResourceMask = val;
            }
            catch
            {
            }
        }
    }
}
