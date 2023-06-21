namespace MatchTile.Powerup
{
    public interface IPowerup
    {
        public bool isLocked { get; }

        public void SetLocked(bool isLocked);
        public void Activate();
    }
}
