public class Gun : MonoBehaviour
{
    public AffectList affectList;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            List<Affect> randomAffects = affectList.GetRandomAffects(3);
            if (randomAffects.Count > 0)
            {
                Affect chosenAffect = randomAffects[Random.Range(0, randomAffects.Count)];
                chosenAffect.ApplyEffect(other.gameObject);
            }
        }
    }
}
