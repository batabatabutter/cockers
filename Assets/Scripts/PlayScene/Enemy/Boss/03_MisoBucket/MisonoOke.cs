using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MisonoOke : Boss
{
    //  UŒ‚‚Ìí—Ş
    private enum AttackType
    {
        Attack01,
        Attack02,
        Attack03,

        OverID
    };
    //  UŒ‚‚Ìè‡
    private enum Attack
    {
        Move01,
        Move02,
        Move03,
        Move04,
        Move05,

        OverID
    };

    //  UŒ‚í—Ş
    private AttackType attackType;
    //  UŒ‚è‡
    private Attack attack;

    //  ƒN[ƒ‹ƒ^ƒCƒ€
    [SerializeField] private float coolTime;
    //  ƒN[ƒ‹ƒ^ƒCƒ}[
    [SerializeField, ReadOnly] private float coolTimer;

    //  UŒ‚”ÍˆÍ
    [SerializeField] private float attackDistance;

    //  •¨—§Œä
    private Rigidbody rd;

    //  ƒWƒƒƒ“ƒv—Í
    [SerializeField] private float jumpForce;

    //  –Ú•W’n“_
    private Vector3 targetPos;

    //  •Ï‰»ó‘Ô
    public enum MisoStatas
    {
        Mix,
        Red,
        White,

        MisoNum
    }
    private MisoStatas misoStatas;

    [SerializeField, Header("ƒ~ƒbƒNƒX–¡‘X"), Label("UŒ‚—Í”{—¦")] float mixAtkRate;
    [SerializeField, Label("UŒ‚‘¬“x”{—¦")] float mixSpdRate;

    [SerializeField, Header("Ô–¡‘X"), Label("UŒ‚—Í”{—¦")] float redAtkRate;
    [SerializeField, Label("UŒ‚‘¬“x”{—¦")] float redSpdRate;

    [SerializeField, Header("”’–¡‘X"), Label("UŒ‚—Í”{—¦")] float whiteAtkRate;
    [SerializeField, Label("UŒ‚‘¬“x”{—¦")] float whiteSpdRate;

    private float atkRate;
    private float spdRate;

    private int bulletCount = 0;
    private float bulletRate = 0.0f;

    //  ’e1
    [SerializeField, Header("–¡‘X’e"), Label("ƒIƒuƒWƒFƒNƒg")] GameObject bullet1Obj;
    [SerializeField, Label("ƒ_ƒ[ƒW")] int bullet1Dmg;
    [SerializeField, Label("‘¬“x")] float bullet1Speed;
    [SerializeField, Label("”­ËŒÂ”")] int bullet1Num;
    [SerializeField, Label("”­Ë‘¬“x")] float bullet1Rate;

    //  ’e2
    [SerializeField, Header("–¡‘X’e(–¡‘X°)"), Label("ƒIƒuƒWƒFƒNƒg")] GameObject bullet2Obj;
    [SerializeField, Label("ƒ_ƒ[ƒW")] int bullet2Dmg;
    [SerializeField, Label("‘¬“x")] float bullet2Speed;
    [SerializeField, Label("“ŠË‘¬“x")] float bullet2UpSpeed;
    [SerializeField, Label("”­ËŒÂ”")] int bullet2Num;
    [SerializeField, Label("”­Ë‘¬“x")] float bullet2Rate;

    //  ‰Šúˆ—
    public override void BossStart()
    {
        attackType = AttackType.OverID;

        attack = Attack.OverID;

        coolTimer = 0f;

        rd = gameObject.GetComponent<Rigidbody>();

        targetPos = Vector3.zero;

        misoStatas = MisoStatas.Mix;
    }

    //  XVˆ—
    public override void BossUpdate()
    {
        switch (misoStatas)
        {
            case MisoStatas.Mix:
                atkRate = mixAtkRate;
                spdRate = mixSpdRate;
                break;
            case MisoStatas.Red:
                atkRate = redAtkRate;
                spdRate = redSpdRate;
                break;
            case MisoStatas.White:
                atkRate = whiteAtkRate;
                spdRate = whiteSpdRate;
                break;
        }

        //  ƒN[ƒ‹ƒ^ƒCƒ€‚ª‘¶İ‚µ‚Ä‚¢‚é
        if (coolTimer > 0f && attackType == AttackType.OverID)
        {
            //  ƒN[ƒ‹ŠÔŒ¸‚ç‚·
            coolTimer -= Time.deltaTime * spdRate;
            //  ˆ—I—¹
            return;
        }

        //  ƒvƒŒƒCƒ„[‚ªUŒ‚”ÍˆÍ“à‚É‚¢‚È‚¢EUŒ‚ó‘Ô‚Å‚Í‚È‚¢
        if (Vector3.Distance(gameObject.transform.position, player.transform.position) > attackDistance &&
            attackType == AttackType.OverID)
        {
            //  ƒvƒŒƒCƒ„[‚ÉŒü‚©‚Á‚ÄˆÚ“®‚·‚é

            //  ˆ—I—¹
            return;
        }
        //  ƒvƒŒƒCƒ„[‚ªUŒ‚”ÍˆÍ“à‚É‚¢‚éEUŒ‚ó‘Ô‚Å‚Í‚È‚¢
        else if (attackType == AttackType.OverID)
        {
            //  UŒ‚‚Ìí—Ş‚ğİ’è‚·‚é
            attackType = (AttackType)Random.Range(0, (int)AttackType.OverID);
            //  UŒ‚è‡‚ğ‚P‚Ö
            attack = Attack.Move01;
            //  ˆ—I—¹
            return;
        }


        //  UŒ‚‚Ìˆ—
        switch (attackType)
        {
            case AttackType.Attack01:
                //  UŒ‚I—¹”»’è
                if (Attack01()) FinishAttack();
                break;
            case AttackType.Attack02:
                //  UŒ‚I—¹”»’è
                if (Attack02()) FinishAttack();
                break;
            case AttackType.Attack03:
                //  UŒ‚I—¹”»’è
                if (Attack03()) FinishAttack();
                break;
        }
    }
    //  UŒ‚‚P(–¡‘XØ‚è‘Ö‚¦)
    private bool Attack01()
    {
        Debug.Log(misoStatas);
        //  –¡‘XØ‚è‘Ö‚¦
        while (true)
        {
            MisoStatas afterMiso = (MisoStatas)Random.Range(0, (int)MisoStatas.MisoNum);
            if (misoStatas != afterMiso)
            {
                misoStatas = afterMiso;
                break;
            }
        }

        //  UŒ‚I—¹
        return true;
    }
    //  UŒ‚‚Q(–¡‘X’e1”­Ë)
    private bool Attack02()
    {
        bulletRate -= Time.deltaTime;
        bulletRate = Mathf.Clamp(bulletRate, 0.0f, float.MaxValue);

        //  ’e¶¬
        if (bulletRate <= 0.0f)
        {
            GameObject obj = Instantiate(bullet1Obj, transform.position, Quaternion.identity);
            obj.GetComponent<MisoBullet>().SetBulletStatas((int)(bullet1Dmg * atkRate), bullet1Speed, -Vector3.Normalize(obj.transform.position - player.transform.position));
            bulletCount++;
            bulletRate = bullet1Rate;
        }

        //  UŒ‚–¢I—¹
        if (bulletCount < bullet1Num) return false;
        //  UŒ‚I—¹
        bulletCount = 0;
        bulletRate = 0.0f;
        return true;
    }
    //  UŒ‚‚R(–¡‘X’e2”­Ë)
    private bool Attack03()
    {
        bulletRate -= Time.deltaTime;
        bulletRate = Mathf.Clamp(bulletRate, 0.0f, float.MaxValue);

        //  ’e¶¬
        if (bulletRate <= 0.0f)
        {
            GameObject obj = Instantiate(bullet2Obj, transform.position, Quaternion.identity);
            obj.GetComponent<MisoBulletCreateFloor>().SetBulletStatas((int)(bullet2Dmg * atkRate), bullet2Speed, bullet2UpSpeed * (bulletCount + 1) / 3, player.transform.position);
            bulletCount++;
            bulletRate = bullet2Rate;
        }

        //  UŒ‚–¢I—¹
        if (bulletCount < bullet2Num) return false;
        //  UŒ‚I—¹
        bulletCount = 0;
        bulletRate = 0.0f;
        return true;
    }
    //  UŒ‚I—¹ˆ—
    private void FinishAttack()
    {
        //  ƒN[ƒ‹ƒ^ƒCƒ€‚ğİ’è
        coolTimer = coolTime;

        //  UŒ‚‚Ìí—Ş‚ğÁ‚·
        attackType = AttackType.OverID;
    }
}
