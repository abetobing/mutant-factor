using UnityEngine;

namespace Characters
{
    [CreateAssetMenu(menuName = "Mutant Factor Character", fileName = "New Character")]
    public class DefaultCharacter : BaseCharacter
    {
        public ProfessionType profession;
    }
}