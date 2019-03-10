public class Sailor_Stats
{
    public int HP { get; set; }
    public int weaponDamage { get; set; }
    public float weaponCooldown { get; set; }

    public Sailor_Stats() {
        this.HP = 50;
        this.weaponDamage = 10;
        this.weaponCooldown = 2f;
    }
}
