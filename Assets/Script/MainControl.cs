using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using System.IO;
using System.Text;
public class MainControl : MonoBehaviour
{
    public AudioSource putong;
    public AudioSource ci;
    public AudioSource tanhuang;
    public AudioSource suilie;
    public AudioSource Bomb;
	public AudioSource GetCoins;
	public AudioSource GetMagicPotions;
    public AudioSource laugh;

    public ArrayList boards;
    public ArrayList bullet;
    public ArrayList bulletmovdirection;
    public ArrayList bomb;
    public ArrayList bombtime;
    public ArrayList breakarray;
    public ArrayList framearray;
    public int frame = 0; // Pause to stop the increment of this var
    float aimovspeed = 2.0f;
    float elfmovspeed = 2.0f;
    private Vector3 movvec = new Vector3(0, 1, 0);
    float boardsUpSpeed = 1.0f;//屏幕刷新频率
    float speed = 5.0f;//人物活动频率
    const float MAXHEIGHT = 5.5f, MINHEIGHT = -5.5f;//客户区底端与顶端
    float currentHeight;//最上砖块的上沿
    private textset blood;//人物血量
    private scoreset score;//分数
    private coinset coin;
    private CameraShake camera_;
    private int bulspeed = 120;
    public SpriteSheet sheet;
    int count = 0;
    //动画
    private GameObject normal_brick;
    private GameObject break_brick;
    private GameObject slide_left_brick;
    private GameObject slide_right_brick;
    private GameObject hurt_brick;
    private GameObject spring_brick;

    private bool is_run;//liao's change for better animation
    private bool is_jumping;
    private bool ready_to_jump;
    public Rigidbody rigidbody_obj;
    public Force_watcher forcetool;
    private int jump_count;
    private bool left_on;
    private bool right_on;
    private bool right_keyup;
    private bool left_keyup;

    private GameObject break_brick_anim;
    private GameObject spring_brick_anim;
    private GameObject anima;
    private GameObject elf;
    private GameObject bullet_explore_anim;
    private GameObject bloodscreen;
    private GameObject blood_screen;
    private GameObject airobot_text;

    private Text tips;

    // Gold Coins
    private GameObject GC;
	private GameObject RealGC;
	private bool isGoldAway;

	// Magic Potions
	private GameObject MP;
	private GameObject RealMP1, RealMP2;
	private int NumberOfMP;
	private bool isMP1Away, isMP2Away;

	// When Player Reach beneath the screen
	private bool ready_to_subHp;

    int[] pre;
    string[] str;
    string filepath;

    private void Awake()
    {
        sheet = GetComponent<SpriteSheet>();
        sheet.AddAnim("stand_left", 2, 0.1, false);
        //sheet.AddAnim("move_death", 12, 1, true);
        sheet.AddAnim("move_right", 9, 1, false);
        sheet.AddAnim("move_left", 9, 1, false);
        sheet.AddAnim("in_air_right", 1, 1, true);
        sheet.AddAnim("in_air_left", 1, 1, true);
        sheet.AddAnim("jump", 2, 0.24, true);
        sheet.AddAnim("stand_right", 2, 0.1, false);

    }

    //private int HP = 100;
    void Start()
    {

        UserInfo.IsPlayGame = true;
        UserInfo.IsPaused = false;


        forcetool = GetComponent<Force_watcher>();//liao's change for better animation
        rigidbody_obj = GetComponent<Rigidbody>();
        is_run = false;
        is_jumping = false;
        ready_to_jump = false;
        jump_count = 0;
        left_on = false;
        right_on = false;
        right_keyup = false;
        left_keyup = false;

        tips = GameObject.Find("Canvas/Tips").GetComponent<Text>();
        tips.CrossFadeAlpha(0, 6, false);

        blood = GameObject.Find("Canvas/blood").GetComponent<textset>();
        score = GameObject.Find("Canvas/score").GetComponent<scoreset>();
        coin = GameObject.Find("Canvas/coin").GetComponent<coinset>();
        camera_ = GameObject.Find("Main Camera").GetComponent<CameraShake>();

        normal_brick = (GameObject)Resources.Load("Prefab/normal_brick");
        break_brick = (GameObject)Resources.Load("Prefab/break_brick");
        slide_left_brick = (GameObject)Resources.Load("Prefab/left_brick");
        slide_right_brick = (GameObject)Resources.Load("Prefab/right_brick");
        hurt_brick = (GameObject)Resources.Load("Prefab/hurt_brick");
        spring_brick = (GameObject)Resources.Load("Prefab/spring_brick");
        bloodscreen = (GameObject)Resources.Load("Prefab/BloodScreen");
        blood_screen = Instantiate(bloodscreen);


        break_brick_anim = (GameObject)Resources.Load("Prefab/break_brick_anim");
        bullet_explore_anim = (GameObject)Resources.Load("Prefab/green_bullet_anim");
        airobot_text = new GameObject("airobot_text", typeof(Text));
        // Gold Coins
        GC = (GameObject)Resources.Load("Prefab/GoldCoin");
		isGoldAway = true;
		// RealGC = Instantiate (GC) as GameObject;
		//RealGC.transform.localScale = new Vector3 (0.55f, 0.55f, 0.8f);
		// RealGC.name = "GoldCoin";

		// Magic Potions
		MP = (GameObject)Resources.Load("Prefab/MagicPotion");

		// Flag of SubHp.
		ready_to_subHp = false;
        NumberOfMP = 0;
		isMP1Away = true;
		isMP2Away = true;
//		RealMP1 = Instantiate (MP) as GameObject;
//		RealMP2 = Instantiate (MP) as GameObject;
//		RealMP1.transform.localScale = new Vector3 (0.2f, 0.2f, 2.0f);
//		RealMP2.transform.localScale = new Vector3 (0.2f, 0.2f, 2.0f);
//		RealMP1.name = "MagicPotionOne";
//		RealMP2.name = "MagicPotionTwo";

       

        boards = new ArrayList();
        bullet = new ArrayList();
        bulletmovdirection = new ArrayList();
        bomb = new ArrayList();
        bombtime = new ArrayList();
        framearray = new ArrayList();
        breakarray = new ArrayList();

        currentHeight = MAXHEIGHT;
        AddBoards(1);

        GameObject obj = Instantiate(normal_brick);
        obj.transform.position = new Vector3(-2.62f, -1.0f, 0);
        boards.Add(obj);

        filepath = Application.dataPath + "/StreamingAssets" + "/score.txt";
        str = File.ReadAllLines(filepath);
        pre = new int[10];
        for (int i = 0; i < 10; i+=1)
            int.TryParse(str[2*i+1], out pre[i]);

    }
    GameObject Generateobj(int id)
    {

        int randomid = Random.Range(1, 12);
        GameObject obj = Instantiate(normal_brick);
        if (obj != null) Destroy(obj);
        switch (randomid)
        {
            case 1: { obj = Instantiate(spring_brick); obj.name = "cube6"; break; } //弹
            case 2: { obj = Instantiate(hurt_brick); obj.name = "cube2"; break; } //刺 
            case 3: { obj = Instantiate(slide_left_brick); obj.name = "cube3"; break; } //传 
            case 4: { obj = Instantiate(slide_right_brick); obj.name = "cube4"; break; } //传
            case 5: { obj = Instantiate(break_brick); obj.name = "cube5"; break; } //碎
            case 6: { obj = Instantiate(slide_left_brick); obj.name = "cube3"; break; };
            case 7: { obj = Instantiate(slide_right_brick); obj.name = "cube4"; break; };

            default: {obj = Instantiate(normal_brick); obj.name = "cube1"; break; } 
        }

        float x;

        x = id == 1 ? Random.Range(-6.0f, -1.5f) : Random.Range(1.5f, 6.0f);

        //obj.transform.localScale = new Vector3(3.5f, 1.5f, 1.0f);
        obj.transform.position = new Vector3(x, currentHeight, 0);

        boards.Add(obj);

		// Gold Coins Appears Here
		//  print(isGoldAway?"True" : "False");
		if (isGoldAway) {
			isGoldAway = false;
            RealGC = Instantiate(GC) as GameObject;
            //RealGC.transform.localScale = new Vector3 (0.55f, 0.55f, 0.8f);
            RealGC.name = "GoldCoin";
            RealGC.transform.position = new Vector3(x, currentHeight + 0.5f, 0);
		}

        // MagicPotions Appears Here
        float randomformp1 = Random.Range(0.0f, 1.0f);
        if (isMP1Away && randomformp1 > 0.75f) {
            isMP1Away = false;
            RealMP1 = Instantiate(MP) as GameObject;

            RealMP1.name = "MagicPotionOne";
            float delta_disx = Random.Range(-0.2f, 0.2f);
            if (delta_disx > 0) delta_disx += 0.7f;
            else delta_disx -= 0.7f;
            RealMP1.transform.position = new Vector3(x + delta_disx, currentHeight + 0.7f, 0);
        }

		
//		if (NumberOfMP == 2) {
			// won't generate any magic potions
//		} else if (NumberOfMP == 1) {
            // generate one bottom of magic potion

  //          float randomnumformp = Random.Range(0.0f, 1.0f);
   //         if(randomnumformp > 0.75f)
   //         {
    //            NumberOfMP++;
    //            float delta_disx = Random.Range(-0.2f, 0.2f);
    //            float delta_disy = Random.Range(-0.2f, 0.2f);
                // adjust the emerging point of magicpotions for more independency
   //             if (delta_disx > 0)
      //              delta_disx += 0.6f;
      //          else
      //              delta_disx -= 0.6f;
      //          if (delta_disy > 0)
      //              delta_disy += 0.6f;
      //          else
      //              delta_disy -= 0.6f;
      
      //          if (isMP1Away)
     //           {
      //              isMP1Away = false;
//
         //           RealMP1 = Instantiate(MP) as GameObject;
          //          RealMP1.transform.localScale = new Vector3(0.2f, 0.2f, 2.0f);

          //          RealMP1.transform.position = new Vector3(x + delta_disx, -2.0f + delta_disy, 0);
          //          RealMP1.name = "MagicPotionOne";
          //      }
           //     else
           //     {
               //     isMP2Away = false;
//
        //            RealMP2 = Instantiate(MP) as GameObject;
         //           RealMP2.transform.localScale = new Vector3(0.2f, 0.2f, 2.0f);
//
          //          RealMP2.transform.position = new Vector3(x + delta_disx, -2.0f + delta_disy, 0);
          //          RealMP2.name = "MagicPotionTwo";
           //     }
         //   }
	//	} else {
        //    float randomnumformp = Random.Range(0.0f, 1.0f);
        //    if (randomnumformp > 0.5f)
          //  {
            //    NumberOfMP++;
         //       float delta_disx = Random.Range(-0.2f, 0.2f);
         //       float delta_disy = Random.Range(-0.2f, 0.2f);
                // double OneOutOfTwo = Random.Range(0.0f, 1.0f);
          //      if (delta_disx > 0)
          //          delta_disx += 0.6f;
          //      else
          //          delta_disx -= 0.6f;
         //       if (delta_disy > 0)
         //           delta_disy += 0.6f;
          //      else
          //          delta_disy -= 0.6f;

      //          isMP1Away = false;
         //       RealMP1 = Instantiate(MP) as GameObject;
         //       RealMP1.transform.localScale = new Vector3(0.2f, 0.2f, 2.0f);

         //       RealMP1.transform.position = new Vector3(x + delta_disx, currentHeight + delta_disy, 0);
         //       RealMP1.name = "MagicPotionOne";
 //               if (OneOutOfTwo < 0.5f)
 //               {
  //                  isMP1Away = false;
  //                  RealMP1.transform.position = new Vector3(x + delta_disx, -2.0f + delta_disy, 0);
  //              }
   //             else
   //             {
  //                  isMP2Away = false;
  //                  RealMP2.transform.position = new Vector3(x + delta_disx, -2.0f + delta_disy, 0);
  //              }
           // }
	//	}
        return obj;
    }
    void AddBoards(int id)
    {
        while (currentHeight > MINHEIGHT)
        {
            score.add();
            if (boardsUpSpeed < 5.0f) boardsUpSpeed += 0.05f;
            int rid = id == 1 ? 2 : (int)Random.Range(0, 3);
            if (rid == 0) Generateobj(1);
            else if (rid == 1) Generateobj(2);
            else { Generateobj(1); Generateobj(2); }
            currentHeight -= 2.5f;
        }
    }
    private void TerminateFunction()
    {

        int rankscore = 10;
        if (score.score > pre[9])
        {
            for (int i = 9; i >= 0 && score.score > pre[i]; i--)
                rankscore = i;
            for (int i = 9; i >= rankscore + 1; i--)
            {
                 str[2 * i + 1] = str[2 * i - 1];
                 str[2 * i] = str[2 * (i - 1)];
             }
            str[2 * (rankscore + 1) - 1] = score.score.ToString();
            str[2 * rankscore] = UserInfo.USER;
            File.WriteAllLines(filepath, str);
        }
        UserInfo.highestscore = score.score;
        SceneManager.LoadScene("End");
    }
    // Update is called once per frame
    void Update()
    {
        // Debug.Log(isGoldAway ? "GoldAway" : "GoldNotAway");
        if (transform.position.y<3.5f)
        {
            for (int i=0;i<boards.Count;i++)
            {
                GameObject obj = (GameObject)boards[i];
                obj.GetComponent<BoxCollider>().enabled = true;
            }
            
        }
        Debug.Log(NumberOfMP);
        // frame++;
        Debug.Log(frame);

        if (!UserInfo.IsPaused) {
            frame++;
        }
        ++count;
        if (count % 15 == 0) Destroy(anima);
        bool haspet = UserInfo.PETS;
		Debug.Log(UserInfo.PETS.ToString());
        Vector3 mainposition = (Vector3)transform.position;
        
        if (frame == 300 && haspet == true)
        {
            GameObject elf_now = (GameObject)Resources.Load("Prefab/elf");
            elf = Instantiate(elf_now);
            elf.transform.localScale = new Vector3(0.8f, 0.8f, 0.1f);
            elf.transform.position = new Vector3(0, 0, 0);
            elf.GetComponent<Renderer>().material.mainTexture = (Texture)Resources.Load("elf");

        }
        if (frame > 300 && haspet == true)
        {

            Vector3 elfposition = (Vector3)elf.transform.position;
            Vector3 elfdirection = mainposition - elfposition;
            elf.transform.Translate(elfdirection * elfmovspeed * Time.deltaTime);
            if (frame % 60 == 0) blood.add2();
        }
        if (transform.position.y < -5.5f || blood.num <= 0)
        {
            //结尾动画
            //GameObject.Find("ObjectName").GetComponent<MainControl>().enabled = false;
            TerminateFunction();
        }

        /*	if (transform.position.y > 5.5f) {
                ready_to_subHp = true;

            }
            if (ready_to_subHp && transform.position.y < 5.5f) {
                ready_to_subHp = false;

                blood.sub20 ();
                camera_.shake();
            }*/

        if ((Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow)))
        {
            transform.Translate(Vector3.right * speed * Time.deltaTime * 2.0f);
            left_on = true;
            if ((forcetool.force.y < 0.05) && (forcetool.force.y > -0.05) && !is_run)
            {
                sheet.Play("move_right");
                is_run = true;
            }


        }
        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            transform.Translate(Vector3.left * speed * Time.deltaTime * 2.0f);
            right_on = true;
            if ((forcetool.force.y < 0.05) && (forcetool.force.y > -0.05) && !is_run)
            {
                sheet.Play("move_left");
                is_run = true;
            }

        }

        if (Input.GetKeyUp(KeyCode.D) || Input.GetKeyUp(KeyCode.RightArrow))
        {
            is_run = false;
            right_on = false;
            right_keyup = true;
            left_keyup = false;
            if ((forcetool.force.y < 0.05) && (forcetool.force.y > -0.05))
            {
                sheet.Play("stand_right");


            }
            else sheet.Play("in_air_right");
        }
        else if (Input.GetKeyUp(KeyCode.A) || Input.GetKeyUp(KeyCode.LeftArrow))
        {
            is_run = false;
            left_on = false;
            right_keyup = false;
            left_keyup = true;
            if ((forcetool.force.y < 0.05) && (forcetool.force.y > -0.05))
            {
                sheet.Play("stand_left");

            }
            else sheet.Play("in_air_left");
        }
        else if (((forcetool.currentVelocity.x < 0.01) && (forcetool.currentVelocity.x > -0.01)) && (forcetool.force.y < 0.05) && (forcetool.force.y > -0.05) && (!is_run))
        {
            if (left_keyup && (!right_keyup)) sheet.Play("stand_left");
            if ((!left_keyup) && right_keyup) sheet.Play("stand_right");
        }
        if ((forcetool.force.y < -0.9) && (right_on) && !is_jumping)
        {
            sheet.Play("in_air_right");
            is_run = false;
        }
        if ((forcetool.force.y < -0.9) && (left_on) && !is_jumping)
        {
            sheet.Play("in_air_left");
            is_run = false;
        }
        if (!is_jumping && (ready_to_jump))
        {
            sheet.Play("jump");
            jump_count = 0;
            is_jumping = true;
        }
        if (is_jumping)
        {
            jump_count++;
        }
        if (jump_count >= 90)
        {
            jump_count = 0;
            is_jumping = false;
            ready_to_jump = false;
        }
        BoardsUp();
        blood.Show();
		coin.Show ();
        //Vector3 mainposition = (Vector3)transform.position;
        //monster ai   

        if (frame == 1800)
        {
            GameObject AI_robot = (GameObject)Resources.Load("Prefab/monster");
            GameObject airobot = Instantiate(AI_robot);
            airobot.transform.localScale = new Vector3(1.5f, 1.5f, 1.0f);
            airobot.transform.position = new Vector3(6.0f, 0, 0);
            airobot.name = "AI";


            if (airobot_text.name != null) {
                airobot_text.AddComponent<TextMesh>();
                airobot_text.transform.localScale = new Vector3(0.05f, 0.05f, 1);
                airobot_text.GetComponent<TextMesh>().text = "Hhhh! Finally, I find you, " + UserInfo.USER + "!";
                //airobot_text.GetComponent<TextMesh>().font = "Fonts/Arial";
                airobot_text.GetComponent<TextMesh>().fontSize = 150;

                airobot_text.transform.localRotation = new Quaternion(0, 180, 0, 0);
                //airobot_text.transform.position = airobot.transform.position;

                Vector3 airobot_pos = airobot.transform.position;

                airobot_pos.x -= 3.0f;
                airobot_pos.y += 2.0f;

                airobot_text.transform.position = airobot_pos;
            }
            //GameObject airobot_text = GameObject.Find("Canvas/ai_text").GetComponent<Text>();

        }
        if (frame == 1800)
        {
            //laugh.volume = 
            laugh.Play();

        }
        if (frame > 1890)
        {
            //airobot_text.name = "noactive";
            if (airobot_text != null) Destroy(airobot_text);
        }
        if (frame>1800)
        { 
            GameObject ai = GameObject.Find("AI");
            if (frame % 480 == 0) ai.transform.position = new Vector3(-ai.transform.position.x, 0, 0);
            if (frame % 60 == 0) movvec = -movvec;
            ai.transform.Translate(movvec * aimovspeed * Time.deltaTime);

         
            Vector3 aiposition = (Vector3)ai.transform.position;
            Vector3 movdirection = mainposition - aiposition;
            if (frame % 60 == 0 && bulspeed>40)
                bulspeed--;

            if (frame % bulspeed == 0)
            {
                GameObject bul_anim = (GameObject)Resources.Load("Prefab/green_bullet");
                GameObject bul = Instantiate(bul_anim);
                bul.transform.localScale = new Vector3(1.50f, 1.50f, 1.0f);

                bul.transform.position = (Vector3)aiposition;
                bul.name = "bullet";

                bullet.Add(bul);
                Vector3 bulletmovvector = (Vector3)movdirection;
                bulletmovdirection.Add((Vector3)bulletmovvector);
            }
            for (int i = 0; i < bulletmovdirection.Count;)
            {
                GameObject obj = (GameObject)bullet[i];
                Vector3 vec = (Vector3)bulletmovdirection[i];
                obj.transform.Translate(vec * Time.deltaTime);
                if (obj.transform.position.x < -7.0f || obj.transform.position.x > 7.0f || obj.transform.position.y > 5.5f || obj.transform.position.y < -5.5f || obj.name == "nonactive")
                {
                    bullet.Remove(obj);
                    Destroy(obj);
                    bulletmovdirection.Remove(vec);
                }
                else i++;

            }
        }
        for (int i = 0; i < framearray.Count;)
        {
            GameObject obj = (GameObject)breakarray[i];
            int preframe = (int)framearray[i];

            if (frame - preframe == 30)
            {
                breakarray.Remove(obj);
                Destroy(obj);
                framearray.Remove(preframe);
            }
            else i++;

        }
        //bullet ai

        for (int i = 0; i < bomb.Count;)
        {
            GameObject obj = (GameObject)bomb[i];

            int preframe = (int)bombtime[i];

            if (frame - preframe == 20)
            {
                bomb.Remove(obj);
                Destroy(obj);
                bombtime.Remove(preframe);

            }
            else i++;

        }

        //elf ai

      
      


 
    }
    void BoardsUp()
    {
        currentHeight += boardsUpSpeed * Time.deltaTime;
        for (int i = 0; i < boards.Count;)
        {

            GameObject obj = (GameObject)boards[i];

            obj.transform.Translate(new Vector3(0, 1, 0) * boardsUpSpeed * Time.deltaTime);
            if (obj.transform.position.y > MAXHEIGHT)
            {
                boards.Remove(obj);
                Destroy(obj);
            }
            else
            {
                i++;
            }
        }
		// Time For Gold Coin Away
		if(!isGoldAway && RealGC.transform.position.y > 4.8f){
			isGoldAway = true;
            // RealGC.transform.position = new Vector3(0.08f, -6.9f, 0);// new Vector3 (10000.0f, 10000.0f, 0);
            Destroy(RealGC);
        }
		else if (!isGoldAway && RealGC.transform.position.y < -5.5f) {
			isGoldAway = true;
            // RealGC.transform.position = new Vector3(0.08f, -6.9f, 0);// new Vector3 (10000.0f, 10000.0f, 0);
            Destroy(RealGC);
        }
		// Time For Magic Potion Away
		if (!isMP1Away && (RealMP1.transform.position.y > 4.8f || RealMP1.transform.position.y < -5.5f)) {
			isMP1Away = true;
            Destroy(RealMP1);
            // RealMP1.transform.position = new Vector3(0.08f, -6.9f, 0);// new Vector3(10000.0f, 10000.0f, 0);
            // NumberOfMP--;
		}
//		if (!isMP2Away && RealMP2.transform.position.y > 4.8f || RealMP2.transform.position.y < -5) {
//			isMP2Away = true;
 //           Destroy(RealMP2);
           //  RealMP2.transform.position = new Vector3(0.08f, -6.9f, 0); //new Vector3(10000.0f, 10000.0f, 0);
 //           NumberOfMP--;
//		}

        AddBoards(2);

    }

    void OnCollisionEnter(Collision thing)
    {
        if (transform.position.y > 4.3f)
        {
            for (int i = 0; i < boards.Count; i++)
            {
                GameObject obj = (GameObject)boards[i];
                obj.GetComponent<BoxCollider>().enabled = false;
            }
            blood.sub20();
            camera_.shake();
            if (blood_screen.GetComponent<BloodScreen>().flag == false) blood_screen.GetComponent<BloodScreen>().flag = true;
        }
        else
        {
            var name = thing.collider.name;
            //Debug.Log("Thing is " + name);
            if (name == "cube2")
            {
                ContactPoint vPoint = thing.contacts[0];
                if (vPoint.normal.y > 0)
                {
                    ci.Play();
                    blood.add();
                    if (blood_screen.GetComponent<BloodScreen>().flag == false) blood_screen.GetComponent<BloodScreen>().flag = true;
                }
            }
            else if (name == "cube3")
            {
                putong.Play();
                //Destroy(thing.collider);

            }
            else if (name == "cube4")
            {
                putong.Play();
            }
            else if (name == "cube5")
            {

                Debug.Log(name);
                ContactPoint vPoint = thing.contacts[0];//获取第一个碰撞点

                Quaternion quate = Quaternion.FromToRotation(Vector3.up, new Vector3(0, 1, 0));//碰撞点法线

                count = 0;
                suilie.Play();
                anima = Instantiate(break_brick_anim, vPoint.point, quate) as GameObject;

                breakarray.Add(anima);
                framearray.Add(frame);

                boards.Remove(thing.gameObject);
                Destroy(thing.gameObject);


            }
            else if (name == "cube6")
            {
                ContactPoint vPoint = thing.contacts[0];
                if (vPoint.normal.y > 0)
                {
                    transform.Translate(new Vector3(0, 8, 0) * speed * Time.deltaTime);
                    is_jumping = false;
                    ready_to_jump = true;
                    tanhuang.Play();
                }
            }
            else if (name == "cube1")
            {
                putong.Play();
            }
            else if (name == "bullet")
            {
                blood.add();
                Bomb.Play();

                GameObject bul_explore = Instantiate(bullet_explore_anim);
                bul_explore.transform.localScale = new Vector3(2.0f, 2.0f, 1.0f);

                ContactPoint vPoint = thing.contacts[0];//获取第一个碰撞点

                Quaternion quate = Quaternion.FromToRotation(Vector3.up, vPoint.normal);//碰撞点法线
                GameObject animation = Instantiate(bul_explore, vPoint.point, quate) as GameObject;
                bomb.Add(animation);
                bombtime.Add(frame);
                thing.gameObject.name = "nonactive";
                Destroy(bul_explore);
            }

            // Collision on GoldCoins
            else if (name == "GoldCoin")
            {
                GetCoins.Play();
                isGoldAway = true;
                // Go to an inaccessible place
                // RealGC.transform.position = new Vector3 (10000.0f, 10000.0f, 0);
                // RealGC.transform.position = new Vector3(0.08f, -6.9f, 0);

                Destroy(RealGC);
                // Coin add one
                coin.Add(1);
            }

            // Collision on MagicPotion
            else if (name == "MagicPotionOne")
            {
                GetMagicPotions.Play();
                isMP1Away = true;
                Destroy(RealMP1);
                //	NumberOfMP--;
                // RealMP1.transform.position = new Vector3(0.08f, -6.9f, 0); //new Vector3 (10000.0f, 10000.0f, 0);

                // HP add some
                blood.add3();

            }
            else if (name == "MagicPotionTwo")
            {
                GetMagicPotions.Play();
                isMP2Away = true;
                Destroy(RealMP2);
                NumberOfMP--;
                RealMP2.transform.position = new Vector3(0.08f, -6.9f, 0);//new Vector3 (10000.0f, 10000.0f, 0);
                                                                          // HP add some
                blood.add3();
            }
        }
    }

    void OnCollisionExit(Collision thing)
    {
        var name = thing.collider.name;
        //Debug.Log("Thing is " + name);
        if (name == "cube2")
        {
            if (blood_screen.GetComponent<BloodScreen>().flag == true) blood_screen.GetComponent<BloodScreen>().flag = false;
            //if (blood_screen != null) Destroy(blood_screen);
        }
        else if (name == "cube3")
        {
            //transform.Translate(Vector3.left * speed * Time.deltaTime * 0.3f);
        }
        else if (name == "cube4")
        {
            //transform.Translate(Vector3.right * speed * Time.deltaTime * 0.3f);
        }
        else if (name == "cube5")
        {
            //
        }
        else if(name == "cube6")
        {
            //thing.gameObject.SetActive(true);
        }
    }

    void OnCollisionStay(Collision thing)
    {
        if (transform.position.y > 4.3f)
        {
            for (int i = 0; i < boards.Count; i++)
            {
                GameObject obj = (GameObject)boards[i];
                obj.GetComponent<BoxCollider>().enabled = false;
            }
            blood.sub20();
            camera_.shake();
            if (blood_screen.GetComponent<BloodScreen>().flag == false) blood_screen.GetComponent<BloodScreen>().flag = true;
        }
        else
        {


            var name = thing.collider.name;
            //Debug.Log("Thing is " + name);
            if (name == "cube2")
            {

            }
            else if (name == "cube3")
            {
                transform.Translate(Vector3.left * speed * Time.deltaTime * 0.3f);
            }
            else if (name == "cube4")
            {
                transform.Translate(Vector3.right * speed * Time.deltaTime * 0.3f);
            }
            else if (name == "cube5")
            {
                //
            }
        }
    }

}
